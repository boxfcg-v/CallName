using RollCall.BLL;
using RollCall.DAL;
using RollCall.DALFactory;
using RollCall.IBLL;
using RollCall.IDAL;
using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace RollCall.UI.Controllers
{
    public class HomeController : BaseController
    {
         Sys_UsersService Sys_user = new Sys_UsersService();
    
        
        
        public ActionResult Index()
        {
            string BU = "PC11";
            LienNumberService lienNumberService = new LienNumberService();
            List<LienNumber> lienNumbe = lienNumberService.GetLienNumberBytype1(BU);
            ViewData["LienNumber"] = lienNumbe;

            string LineName = "S04";
            if (Request["linename"] != null)
                LineName = Request["linename"].ToString();
            StaffService staffService = new StaffService();
            List<Staff> staff = staffService.GetStaffByLineName(LineName,BU);
            ViewData["Staff"] = staff;
            return View();
        }

        public ActionResult Login(string UserID1, string Password1)
        {
            IStaffDal staffDal = new StaffDal();
            //ViewBag.Message = "Your application description page.";
            Sys_Users user = new Sys_Users();
            PhoneUsers phoneuser = new PhoneUsers();
            bool flag = true; //驗證標記
            if (UserID1 != null && Password1 != null) {
                

                //驗證PhoneUser 上有沒有數數據
               var temp= Sys_user.DbSession.PhoneUsersDal.GetEntity(u => u.ID== UserID1&&u.PASSWORD1== Password1);
              
                if ( temp.Count() == 1)
                {
                    flag = false;
                    PhoneUsers pu = new PhoneUsers();
                    foreach (PhoneUsers t in temp)
                    {
                        var temp1 = Sys_user.DbSession.StaffDal.GetEntity(u => u.ID == UserID1 );
                        foreach (Staff a in temp1)
                        {
                            //Session["Users"] = temp1 as Staff;
                            Session["UserID"] = a.ID;
                            Session["UserBU"] = a.BU;
                            Session["UserCLASS"] = a.CLASS;
                            Session["UserLINENAME"] = a.LINENAME;
                        }
                        pu = t;
                    }
                    //線長
                    if (pu.power1 == "1")
                    {
                        return RedirectToAction("../Phone/CallNameSelectClass");
                    }
                    //看明細的管理人員
                    if (pu.power1 == "3")
                    {
                        return RedirectToAction("../details/QCdetail");
                    }
                }

                //驗證文員賬號
                user =Sys_user.GetSys_Users(UserID1, Password1);
                if (user.Emp_no != null)
                {
                    flag = false;
                    Session["BU"] = user.Item3;
                   // BU = user.Item3;
                    return RedirectToAction("../Staff/StaffSearch");
                   // return  View("../Staff/StaffSearch");
                }
                if (flag)
                {
                    string msg = "验证失败";
                    var script = string.Format("alert('{0}');", msg);
                    //return JavaScript(script);
                    return Content("<script>alert('验证失败');history.go(-1);</script>");

                }
                   
                
            }
            return View(); 
            //return Content("密碼錯誤");
        }

     


        public ActionResult Staff()
        {
            string BU = "PC11";
            string LineName = "S04";
            if(Request["linename"]!=null)
            LineName = Request["linename"].ToString();
            StaffService staffService = new StaffService();
            List<Staff> staff = staffService.GetStaffByLineName(LineName,BU);
            ViewData["Staff"] = staff;
            return View();
        }
        
        public  ActionResult Userr()
        {
            List<Sys_Users> user = Sys_user.DbSession.Sys_UsersDal.GetEntity(u => true).ToList();

            ViewData["Sys_Users"] = user;
           
            return View();
        }
        #region 特殊線長
        public ActionResult SearchUrrs(string id)

        {
            string sql = "select cpf01,cpf02,cpf29,cpf70 from cpf_file where  cpf35 is null and  cpf01='" + id + "' ";

            DataTable dt = new DataTable();
            dt = TIPTOP.AdoDAL.SQLHelper.GetTable(sql, System.Data.CommandType.Text);
            string a1, a2, a3;
            try
            {
                a1 = dt.Rows[0][1].ToString();//工號 姓名 部門入廠日期
                a2 = dt.Rows[0][2].ToString();//工號 姓名 部門入廠日期
                a3 = dt.Rows[0][3].ToString().Substring(0, 9);//工號 姓名 部門入廠日期
                return Content(a1 + "," + a2 + "," + a3);
            }
            catch
            {
                a1 = "沒查找到此工號對應的員工";
                a2 = "沒查找到此工號對應的員工";
                a3 = "沒查找到此工號對應的員工";
                return Content(a1 + "," + a2 + "," + a3);
            }
        }
        public ActionResult Userrdelete(string id)
        {


            try
            {
                string[] uid = id.Split(',');
                foreach (string i in uid)
                {
                    if (!string.IsNullOrEmpty(i))
                    {
                        Sys_Users spc = Sys_user.DbSession.Sys_UsersDal.FirstOrDefault(u => u.Emp_no == i.ToString().Trim());
                        Sys_user.DbSession.Sys_UsersDal.Delete(spc);
                        PhoneUsers pu = Sys_user.DbSession.PhoneUsersDal.FirstOrDefault(u => u.ID == i.ToString().Trim());
                        if (pu != null)
                        {
                            Sys_user.DbSession.PhoneUsersDal.Delete(pu);
                        }
                    }
                }
                Sys_user.DbSession.SaveChanges();

                return Content("刪除成功");

            }
            catch (Exception)
            {

                return Content("刪除失敗");
            }
        }

        public ActionResult Userradd(string id, string bu, string ps)
        {
            if (id != null)
            {
                try
                {
                    string sql = "select cpf01,cpf02,cpf29,cpf70 from cpf_file where  cpf35 is null and  cpf01='" + id + "' ";

                    DataTable dt = new DataTable();
                    dt = TIPTOP.AdoDAL.SQLHelper.GetTable(sql, System.Data.CommandType.Text);
                    Sys_Users spc = new Sys_Users();
                    spc.Emp_no = dt.Rows[0][0].ToString();
                    spc.Name = dt.Rows[0][1].ToString();
                    spc.Password = dt.Rows[0][0].ToString();
                    spc.Department = dt.Rows[0][2].ToString();
                    spc.Item3 = bu;
                    spc.Item1 = ps;

                    Sys_user.DbSession.Sys_UsersDal.Add(spc);
                    if (ps == "主管")
                    {
                        PhoneUsers pu = new PhoneUsers();
                        pu.ID = id.ToString().Trim();
                        pu.PASSWORD1 = id.ToString().Trim();
                        pu.power1 = "3";
                        Sys_user.DbSession.PhoneUsersDal.Add(pu);
                    }
                    Sys_user.DbSession.SaveChanges();
                    return Content("添加成功");
                }
                catch (Exception)
                {

                    return Content("沒有這個員工");
                }

            }
            return Content("沒有這個員工");
        }


    

        #endregion
    }
}