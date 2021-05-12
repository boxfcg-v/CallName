
using RollCall.BLL;
using RollCall.IBLL;
using RollCall.Model;
using RollCall.UI.App_Start;
using RollCall.UI.Commo;
using RollCall.UI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIPTOP.BLL;
using TIPTOP.Model;

namespace RollCall.UI.Controllers
{
    [LoginFilter]
    public class StaffController : BaseController
    {
        StaffService staffService = new StaffService();
        PhoneUsersService phoneUserService = new PhoneUsersService();
        // string BU ="PE17";

        // GET: Staff

        //顯示全廠員工
        public ActionResult StaffAllSearch()
        {
            // string BU = "PE17";
            StaffService staffService = new StaffService();
            List<Staff> staff = staffService.GetStaffAll();
            ViewData["Staff"] = staff;

            return View();
        }

        //顯示文員所在部門員工
        public ActionResult StaffSearch()
        {
           // string BU = "PE17";
            StaffService staffService = new StaffService();
            List<Staff> staff = staffService.GetStaffAll(BU);
            ViewData["Staff"] = staff;
          
            return View();
        }

        //添加員工頁面 更具條件搜索員工功能  
        public ActionResult StaffAdd()
        {
            string ID = Request.Form["ID"];
            string BU = Request.Form["BU"];
            string RData = Request.Form["RData"];
            cpf_fileService cpfService = new cpf_fileService();
            List<cpf_file> cpf = cpfService.Getcpf_fileByIdBuDate(ID, BU, RData);
            //過濾已經存在的員工
            Commoo comm = new Commoo();
            List<cpf_file> newcpf = new List<cpf_file>();
            if (cpf != null)
            { 
                 newcpf = comm.filtercpf(cpf);
            }

            ViewData["cpf"] = newcpf;
            return View();
        }

        //添加員工頁面 添加員工功能  
        public ActionResult AddToStaff(string[] UserId,string ps,string cls,string ln)
        {
            //string BU = "PE17";
            List<string> list = new List<string>();
            List<cpf_file> cpf = new List<cpf_file>();
            List<Staff> staff = new List<Staff>();
            List<PhoneUsers> phoneUsers = new List<PhoneUsers>();
            if (UserId == null) return Content("請選擇要添加的數據");
            foreach (var item in UserId)
            {
                list.Add(item.ToString());
            }
            StaffService staffService = new StaffService();
            cpf_fileService cpfService = new cpf_fileService();
            cpf = cpfService.Getcpf_fileBylistId(list);
            Commo.Commoo comm = new Commo.Commoo();

            staff=comm.GetStaffList(cpf, BU, cls, ln, ps);
            //線長添加手機點名賬號
            if (ps == "線長")
            {
                phoneUsers = comm.GetPhoneUsersList(cpf);
                PhoneUsersService phoneUsersService = new PhoneUsersService();
                phoneUsersService.addPhoneUsersList(phoneUsers);
            }
            int number = staffService.addStaffList(staff);
            if(number>0)
            { 
                return Content("OK 成功添加 "+ number + "條數據");
            }
            else
            { 
                return Content("添加失敗 人員已存在");
            }

        }


        //顯示線長的頁面
        public ActionResult StaffLineBoss(string id)
        {
            string linename = id;
            // string BU = "PC11";
            StaffService staffService = new StaffService();
            List<Staff> staff = staffService.GetStaffLineBoss(BU);
            ViewData["Staff"] = staff;
            ViewData["linename"] = linename;
            return View();
        }

        //按線名顯示不同線別員工
        public ActionResult StaffLine(string id)
        {
            string linename = id;
           // string BU = "PC11";
            StaffService staffService = new StaffService();
            List<Staff> staff = staffService.GetStaffByLineName(linename,BU);
      
            ViewData["Staff"] = staff;
            ViewData["linename"] = linename;
            return View();
        }

      
        public JsonResult GetDepartments()
        {
            List<LienNumber> lienNumbea = (List<LienNumber>)ViewData["LienNumber"];

            List<Department> lstRes = (List<Department>)ViewData["Department"];
            return Json(lstRes, JsonRequestBehavior.AllowGet);
        }

        //刪除與更換白晚班
        public ActionResult CrudStaff(string Uid , string act)
        {
            int a = 0;
            if(act=="Del")
            { 
                Uid = Uid.ToString().Trim().TrimEnd(',');
                string[] str = Uid.ToString().Split(',');
                List<string> uuid = new List<string>();

                foreach (string s in str)
                {
                    uuid.Add(s);
                }
                a = staffService.delStaff(uuid);
                //刪除線長的賬號密碼
                phoneUserService.delPhoneUsers(uuid);


            }

            if (act == "UpdateClass")
            {
                Uid = Uid.ToString().Trim().TrimEnd(',');
                string[] str = Uid.ToString().Split(',');
                List<string> uuid = new List<string>();

                foreach (string s in str)
                {
                    uuid.Add(s);
                }
                a = staffService.UpdateClass(uuid);
            }
           
           // return View();
            return Content("OK 刪除" + a + "條數據成功");
        }

        //換線別
        public ActionResult Staff_TO_linename(string id)
        {
            if (id != null)
            {
                string[] Uid = id.Split(',');
                string id1 = Uid[0];
                string linename = Uid[1];
                foreach (Department dt in (List<Department>)ViewData["Department"])
                {
                    if (dt.ID == linename)
                    {
                        linename = dt.Name;
                    }
                }
             var temp=   staffService.DbSession.StaffDal.FirstOrDefault(u => u.ID == id1);
                temp.LINENAME = linename;
                staffService.DbSession.SaveChanges();
            }
            return Content("OK");
        }

        //根據工號搜索員工信息
        public ActionResult SearchTIPTOPStaff(string id)
        {
            string sql= "select cpf01,cpf02,cpf29,cpf70 from cpf_file where  cpf35 is null and  cpf01='" + id + "' ";
          
            DataTable dt = new DataTable();
            dt = TIPTOP.AdoDAL.SQLHelper.GetTable(sql, System.Data.CommandType.Text);
            string a1, a2, a3;
            try
            {
                 a1 = dt.Rows[0][1].ToString();//工號 姓名 部門入廠日期
                 a2 = dt.Rows[0][2].ToString();//工號 姓名 部門入廠日期
                 a3 = dt.Rows[0][3].ToString().Substring(0, 9);//工號 姓名 部門入廠日期
            }
            catch {
                 a1 = "沒查找到此工號對應的員工";
                 a2 = "沒查找到此工號對應的員工";
                 a3 = "沒查找到此工號對應的員工";
            }
           
          
            return Content(a1+ "," + a2+","+a3);
        }

        //添加員工
        public ActionResult StaffADDD(string id,string class1,string li)
        {
            if (id != null)
            {
                try
                {
                    string sql = "select cpf01,cpf02,cpf29,cpf70 from cpf_file where  cpf35 is null and  cpf01='" + id + "' ";
                    DataTable dt = new DataTable();
                    dt = TIPTOP.AdoDAL.SQLHelper.GetTable(sql, System.Data.CommandType.Text);
                    Staff sf = new Staff();
                    sf.ID = id;
                    sf.NAME = dt.Rows[0][1].ToString();
                    sf.BU = dt.Rows[0][2].ToString();
                    sf.BUID = dt.Rows[0][2].ToString();
                    sf.CLASS = class1;
                    sf.POSITION = "線員";
                    sf.LINENAME = li;
                    sf.date1 = DateTime.Parse(dt.Rows[0][3].ToString().Substring(0, 9));
                    sf.date2 = DateTime.Now.Date;
                    staffService.DbSession.StaffDal.Add(sf);
                    staffService.DbSession.SaveChanges();
                    return Content("添加成功");
                }
                catch (Exception)
                {

                    return Content("添加失敗可能該員工已經存在");
                }
                
            }
            return Content("添加失敗可能該員工已經存在");

        }

        //添加線長
        public ActionResult StaffADDDBOSS(string id, string class1, string li)
        {
            if (id != null)
            {
                try
                {
                    string sql = "select cpf01,cpf02,cpf29,cpf70 from cpf_file where  cpf35 is null and  cpf01='" + id + "' ";
                    DataTable dt = new DataTable();
                    dt = TIPTOP.AdoDAL.SQLHelper.GetTable(sql, System.Data.CommandType.Text);
                    Staff sf = new Staff();
                    sf.ID = id;
                    sf.NAME = dt.Rows[0][1].ToString();
                    sf.BU = dt.Rows[0][2].ToString();
                    sf.BUID = dt.Rows[0][2].ToString();
                    sf.CLASS = class1;
                    sf.POSITION = "線長";
                    sf.LINENAME = li;
                    sf.date1 = DateTime.Parse(dt.Rows[0][3].ToString().Substring(0, 9));
                    sf.date2 = DateTime.Now.Date;
                    staffService.DbSession.StaffDal.Add(sf);
                    PhoneUsers pu = new PhoneUsers();
                    pu.ID = id;pu.PASSWORD1 = id;pu.power1 = "1";
                    staffService.DbSession.PhoneUsersDal.Add(pu);
                    staffService.DbSession.SaveChanges();
                    return Content("添加成功");
                }
                catch (Exception)
                {

                    return Content("添加失敗可能該員工已經存在");
                }

            }
            return Content("添加失敗可能該員工已經存在");

        }

        #region 特殊線長
        //添加特殊線長
        public ActionResult StaffXXXBOSS()
        {


            List<SpecialCase> specialCase = staffService.DbSession.SpecialCaseDal.GetEntity(u => u.BU == BU).ToList();

            ViewData["SpecialCase"] = specialCase;
            return View();
        }

   
        //根據工號搜索員工信息
        public ActionResult SearchStaffBOSS(string id)
        {


            Staff sf = staffService.DbSession.StaffDal.FirstOrDefault(u => u.ID == id &&u.POSITION=="線長");
            string a1, a2, a3;
            try
            {
                a1 = sf.NAME;// 姓名 部門 線別
                a2 = sf.BUID;
                a3 = sf.LINENAME;
            }
            catch
            {
                a1 = "沒查找到此工號對應的線長";
                a2 = "沒查找到此工號對應的線長";
                a3 = "沒查找到此工號對應的線長";
            }


            return Content(a1 + "," + a2 + "," + a3);
        }
        public ActionResult StaffXXXBOSSadd(string id, string code, string note)
        {
            if (id != null)
            {
                try
                {
                  
                   
                    Staff sf= staffService.DbSession.StaffDal.FirstOrDefault(u=>u.ID==id);
                    SpecialCase spc = new SpecialCase();
                    spc.ID = sf.ID;
                    spc.NAME = sf.NAME;
                    spc.BU = sf.BU;
                    spc.BUID = sf.BUID;
                    spc.LINENAME = sf.LINENAME;
                    spc.CODE = code;
                    spc.note = note;                   
                    staffService.DbSession.SpecialCaseDal.Add(spc);
                    staffService.DbSession.SaveChanges();
                    return Content("添加成功");
                }
                catch (Exception)
                {

                    return Content("沒有此線長");
                }

            }
            return Content("沒有此線長");
        }

        public ActionResult StaffXXXBOSSdelte(string id)
        {

            try {
                string[] uid = id.Split(',');
                foreach (string i in uid)
                { 
                    if(!string.IsNullOrEmpty(i))
                    { 
                        SpecialCase spc = staffService.DbSession.SpecialCaseDal.FirstOrDefault(u => u.ID == i.ToString().Trim());
                        staffService.DbSession.SpecialCaseDal.Delete(spc);
                    }
                }
                staffService.DbSession.SaveChanges();
            
                return Content("刪除成功");
                
             }
             catch (Exception)
             {

                 return Content("刪除失敗");
             }
        }

        #endregion

        #region 特殊線員
        //根據工號搜索員工信息
        public ActionResult StaffXXXLISE()
        {


            List<StaffXXX> staffXXX = staffService.DbSession.StaffXXXDal.GetEntity(u => u.BU == BU).ToList();

            ViewData["StaffXXX"] = staffXXX;
            return View();
        }
        public ActionResult SearchStaffLine(string id)
        {


            Staff sf = staffService.DbSession.StaffDal.FirstOrDefault(u => u.ID == id );
            string a1, a2, a3;
            try
            {
                a1 = sf.NAME;// 姓名 部門 線別
                a2 = sf.BUID;
                a3 = sf.LINENAME;
            }
            catch
            {
                a1 = "沒查找到此工號對應的員工";
                a2 = "沒查找到此工號對應的員工";
                a3 = "沒查找到此工號對應的員工";
            }
            return Content(a1 + "," + a2 + "," + a3);
        }
        public ActionResult StaffXXLINEadd(string id, string code, string note)
        {
            if (id != null)
            {
                try
                {


                    Staff sf = staffService.DbSession.StaffDal.FirstOrDefault(u => u.ID == id);
                    SpecialCase spc = new SpecialCase();
                    spc.ID = sf.ID;
                    spc.NAME = sf.NAME;
                    spc.BU = sf.BU;
                    spc.BUID = sf.BUID;
                    spc.LINENAME = sf.LINENAME;
                    spc.CODE = code;
                    spc.note = note;
                    staffService.DbSession.SpecialCaseDal.Add(spc);
                    staffService.DbSession.SaveChanges();
                    return Content("添加成功");
                }
                catch (Exception)
                {

                    return Content("沒有此線長");
                }

            }
            return Content("沒有此線長");
        }

        public ActionResult StaffXXXLinedelte(string id)
        {

            try
            {
                string[] uid = id.Split(',');
                foreach (string i in uid)
                {
                    if (!string.IsNullOrEmpty(i))
                    {
                        SpecialCase spc = staffService.DbSession.SpecialCaseDal.FirstOrDefault(u => u.ID == i.ToString().Trim());
                        staffService.DbSession.SpecialCaseDal.Delete(spc);
                    }
                }
                staffService.DbSession.SaveChanges();

                return Content("刪除成功");

            }
            catch (Exception)
            {

                return Content("刪除失敗");
            }
        }

        #endregion



    }
}