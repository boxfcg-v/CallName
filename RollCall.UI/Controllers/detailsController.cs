using RollCall.BLL;
using RollCall.IBLL;
using RollCall.Model;
using RollCall.UI.Commo;
using RollCall.UI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RollCall.UI.Controllers
{
    public class detailsController : BasePhoneController
    {
        IAttendanceService attendanceService = new AttendanceService();
        DetailsCommon common = new DetailsCommon();
        // GET: details
        public ActionResult Index()
        {
            return View();
        }
        //全廠明細
        public ActionResult QCdetail(string id)
        {
            #region  查詢時間 設置
            if (id == null) id = "0";
            DateTime now = DateTime.Now.AddDays(int.Parse(id));
            string today = now.Date.ToShortDateString();
            DateTime start = Convert.ToDateTime(today.Trim() + " " + "00:00:00");
            DateTime end = Convert.ToDateTime(today.Trim() + " " + "23:59:59");

            //搜索
            if (Request.Form["RData"] != null && Request.Form["RData"].ToString().Length == 8)
            {
                if (common.IsDate(Request.Form["RData"].ToString()))
               { 
                    string RData = Request.Form["RData"];
                    now = DateTime.ParseExact(RData, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    today = now.Date.ToShortDateString();
                    start = Convert.ToDateTime(today.Trim() + " " + "00:00:00");
                    end = Convert.ToDateTime(today.Trim() + " " + "23:59:59");
                    id = (now-DateTime.Now).Days.ToString();
                };

            }
            #endregion

            #region  獲取全廠的出勤
            List<QCdetail> qcdetails = new List<QCdetail>();
            QCdetail qcdetail = new QCdetail();
         
         
            //有多少線
            var Lists = attendanceService.DbSession.LienNumberDal.GetEntity(u => true).OrderBy(u=>u.OrderbBy).Select(u => u.type1).ToList();
            Lists = Lists.Distinct().ToList();
            //下面的四個部門統稱為 PM 要做特殊添加
            //Lists.Add("PM11"); Lists.Add("PM12"); Lists.Add("PM14"); Lists.Add("PM31");
            //編制人力
            GigabyteFrameEntities db = new GigabyteFrameEntities();
            var bz= db.DepartmentCompilation.Where(u => Lists.Contains(u.department)).Sum(u => u.nums);

            //獲取全廠的出勤
            var BZ = int.Parse(bz.ToString());
           
            var SJ = attendanceService.DbSession.StaffDal.GetEntity(u => true).Count();
            //獲取今天出去明細
            var tatol = attendanceService.GetEntity(u => u.date1 > start && u.date1 < end);
            //return db.Set<T>().Where(whereLambda).AsQueryable();
            var cq = tatol.Where(u => u.date1 > start && u.date1 < end && u.state1 == "到").AsQueryable().Count();
            var cd = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason1 == "遲到").Count();
            var qj = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason1 == "請假").Count();
            var kg = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason1 == "曠工").Count();
            var zsb = tatol.Where(u => u.date1 > start && u.date1 < end && u.state2 == "中途上班").Count();
            var zzt = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason2 == "早退").Count();
            var zqj = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason2 == "請假").Count();
            var zkg = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason2 == "曠工").Count();
            var time = tatol.Where(u => u.date1 > start && u.date1 < end && u.time1 != null).
                Sum(u => u.time1);
            //var cq = attendanceService.CurrentDal.GetEntity(u => u.date1 > start && u.date1 < end && u.state1 == "到").Count();
            //var cd = attendanceService.CurrentDal.GetEntity(u => u.date1 > start && u.date1 < end && u.reason1 == "遲到").Count();
            //var qj = attendanceService.CurrentDal.GetEntity(u => u.date1 > start && u.date1 < end && u.reason1 == "請假").Count();
            //var kg = attendanceService.CurrentDal.GetEntity(u => u.date1 > start && u.date1 < end && u.reason1 == "曠工").Count();
            //var zsb = attendanceService.CurrentDal.GetEntity(u => u.date1 > start && u.date1 < end && u.state2 == "中途上班").Count();
            //var zzt = attendanceService.CurrentDal.GetEntity(u => u.date1 > start && u.date1 < end && u.reason2 == "早退").Count();
            //var zqj = attendanceService.CurrentDal.GetEntity(u => u.date1 > start && u.date1 < end && u.reason2 == "請假").Count();
            //var zkg = attendanceService.CurrentDal.GetEntity(u => u.date1 > start && u.date1 < end && u.reason2 == "曠工").Count();
            //var time = attendanceService.CurrentDal.GetEntity(u => u.date1 > start && u.date1 < end && u.time1 != null).
            //    Sum(u => u.time1);
            qcdetail.BUID = "全廠";
            qcdetail.BZ = BZ; qcdetail.SJ = SJ;
            qcdetail.cq = cq; qcdetail.cd = cd; qcdetail.qj = qj;
            qcdetail.kg = kg; qcdetail.zsb = zsb; qcdetail.zzt = zzt;
            qcdetail.zqj = zqj; qcdetail.zkg = zkg;
            if(time!=null)qcdetail.time1 = double.Parse(time.ToString());
            qcdetails.Add(qcdetail);
            #endregion

            #region   個部門的出勤情況
            //個部門的出勤情況
            foreach (string bu in Lists)
            {
                QCdetail qcdetail1 = new QCdetail();
                //更具PM，特殊情況處理
                int BZ1 = 0;
                if (bu == "PM")
                {
                     BZ1 = db.DepartmentCompilation.Where(u => u.department == "PM11"|| u.department == "PM12" || u.department == "PM14" || u.department == "PM31").Sum(u => u.nums);
                }
                else {
                     BZ1 = db.DepartmentCompilation.Where(u => u.department == bu).Sum(u => u.nums);
                }
                //var BZ1 = db.DepartmentCompilation.Where(u => u.department == bu).Sum(u => u.nums);
                var SJ1 = attendanceService.DbSession.StaffDal.GetEntity(u => u.BU == bu).Count();
                var cq1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.state1 == "到" && u.BU == bu).Count();
                var cd1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason1 == "遲到" && u.BU == bu).Count();
                var qj1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason1 == "請假" && u.BU == bu).Count();
                var kg1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason1 == "曠工" && u.BU == bu).Count();
                var zsb1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.state2 == "中途上班" && u.BU == bu).Count();
                var zzt1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason2 == "早退" && u.BU == bu).Count();
                var zqj1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason2 == "請假" && u.BU == bu).Count();
                var zkg1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason2 == "曠工" && u.BU == bu).Count();
                var time1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.time1 != null && u.BU == bu).Sum(u => u.time1);

                qcdetail1.BUID = bu;
                qcdetail1.BZ = BZ1; qcdetail1.SJ = SJ1;
                qcdetail1.cq = cq1; qcdetail1.cd = cd1; qcdetail1.qj = qj1;
                qcdetail1.kg = kg1; qcdetail1.zsb = zsb1; qcdetail1.zzt = zzt1;
                qcdetail1.zqj = zqj1; qcdetail1.zkg = zkg1;
                if (time1 != null) qcdetail1.time1 = double.Parse(time1.ToString());
                qcdetails.Add(qcdetail1);
            }
            #endregion

            ViewData["qcdetail"] = qcdetails;
            ViewData["date_int"] = id;
            ViewData["Date"] = now.ToString("yyyy/MM/dd");

           
            return View();
        }


        //個部門明細
        public ActionResult BUdetail(string id)
        {
            List<BUdetail> list_budetails = new List<BUdetail>();
            List<BUdetail> list_budetail_total = new List<BUdetail>();
            QCdetail qcdetail = new QCdetail();
            GigabyteFrameEntities db = new GigabyteFrameEntities();

            string bu = id;//部門
            string CLASS = "";//班別
            string id1 = "0";//上下天加的數
            if (id1 == null) id1 = "0";
            string[] uid = id.ToString().Split(',');
            if (uid.Count() == 2)
            {
                bu = uid[0];
                id1 = uid[1];
            }

            #region  上一天，下一天 搜索 查詢時間 設置
          
          
            DateTime now = DateTime.Now.AddDays(int.Parse(id1));
            string today = now.Date.ToShortDateString();
            DateTime start = Convert.ToDateTime(today.Trim() + " " + "00:00:00");
            DateTime end = Convert.ToDateTime(today.Trim() + " " + "23:59:59");

            //搜索時間設置
            if (Request.Form["RData"] != null && Request.Form["RData"].ToString().Length == 8)
            {
                if (common.IsDate(Request.Form["RData"].ToString()))
                {
                    string RData = Request.Form["RData"];
                    now = DateTime.ParseExact(RData, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    today = now.Date.ToShortDateString();
                    start = Convert.ToDateTime(today.Trim() + " " + "00:00:00");
                    end = Convert.ToDateTime(today.Trim() + " " + "23:59:59");
                    id1 = (now - DateTime.Now).Days.ToString();
                };

            }
            #endregion


            //查詢出今天出勤
            var tatol = attendanceService.GetEntity(u => u.date1 > start && u.date1 < end && u.BU == bu);
            if (bu == "PM")
            {
                 tatol = attendanceService.GetEntity(u => u.date1 > start && u.date1 < end && u.BU.Contains(bu));
            }

            for (int i = 0; i < 2; i++)
            {
                if (i == 0) { CLASS = "白班"; } else { CLASS = "晚班"; }
                #region  總出勤狀況
                //var YD1 = attendanceService.DbSession.StaffDal.GetEntity(u => u.BU == bu && u.CLASS==CLASS).Count();

                //var SD1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.state1 == "到" && u.BU == bu && u.CLASS == CLASS).Count();
                //var cd1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason1 == "遲到" && u.BU == bu && u.CLASS == CLASS).Count();
                //var qj1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason1 == "請假" && u.BU == bu && u.CLASS == CLASS).Count();
                //var kg1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason1 == "曠工" && u.BU == bu && u.CLASS == CLASS).Count();
                //var zsb1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.state2 == "中途上班" && u.BU == bu && u.CLASS == CLASS).Count();
                //var zzt1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason2 == "早退" && u.BU == bu && u.CLASS == CLASS).Count();
                //var zqj1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason2 == "請假" && u.BU == bu && u.CLASS == CLASS).Count();
                //var zkg1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.reason2 == "曠工" && u.BU == bu && u.CLASS == CLASS).Count();
                //var time1 = tatol.Where(u => u.date1 > start && u.date1 < end && u.time1 != null && u.BU == bu && u.CLASS == CLASS).Sum(u => u.time1);

                var YD1 = attendanceService.DbSession.StaffDal.GetEntity(u => u.BU == bu && u.CLASS == CLASS).Count();
                if (bu == "PM")
                {
                    YD1 = attendanceService.DbSession.StaffDal.GetEntity(u => u.BU.Contains(bu) && u.CLASS == CLASS).Count();
                }

                var SD1 = tatol.Where(u => u.state1 == "到" && u.CLASS == CLASS).Count();
                var cd1 = tatol.Where(u => u.reason1 == "遲到" && u.CLASS == CLASS).Count();
                var qj1 = tatol.Where(u => u.reason1 == "請假" && u.CLASS == CLASS).Count();
                var kg1 = tatol.Where(u => u.reason1 == "曠工" && u.CLASS == CLASS).Count();
                var zsb1 = tatol.Where(u => u.state2 == "中途上班" && u.CLASS == CLASS).Count();
                var zzt1 = tatol.Where(u => u.reason2 == "早退" && u.CLASS == CLASS).Count();
                var zqj1 = tatol.Where(u => u.reason2 == "請假" && u.CLASS == CLASS).Count();
                var zkg1 = tatol.Where(u => u.reason2 == "曠工" && u.CLASS == CLASS).Count();
                var time1 = tatol.Where(u => u.time1 != null && u.CLASS == CLASS).Sum(u => u.time1);
                BUdetail BUdetail = new BUdetail();

                BUdetail.BUID = bu; BUdetail.CLASS = CLASS; BUdetail.YD = YD1;
                BUdetail.SD = SD1; BUdetail.cd = cd1; BUdetail.qj = qj1; BUdetail.kg = kg1;
                BUdetail.zsb = zsb1; BUdetail.zzt = zzt1; BUdetail.zqj = zqj1; BUdetail.zkg = zkg1;
                if (time1 != null) BUdetail.time1 = double.Parse(time1.ToString());
                BUdetail.BUIDID = attendanceService.DbSession.LienNumberDal.FirstOrDefault(u => u.type1 == bu).id;
                list_budetail_total.Add(BUdetail);
                #endregion
            }
                // 查詢線別
                var lists = attendanceService.DbSession.LienNumberDal.GetEntity(u => u.type1 == bu)
                                            .OrderBy(u => u.OrderbBy).Select(u => u.linename);
                bool isfirst = true;
                if (lists.Count() == 1) { isfirst=false; }
            if (isfirst)
            {
                #region  個線出勤狀況
                foreach (string linename in lists)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 0) { CLASS = "白班"; } else { CLASS = "晚班"; }
                        var YD2 = attendanceService.DbSession.StaffDal.GetEntity(u => u.BU == bu && u.CLASS == CLASS && u.LINENAME == linename).Count();
                        if (bu == "PM")
                        {
                            YD2 = attendanceService.DbSession.StaffDal.GetEntity(u => u.BU.Contains(bu) && u.CLASS == CLASS && u.LINENAME == linename).Count();
                        }
                        var SD2 = tatol.Where(u => u.state1 == "到" && u.CLASS == CLASS && u.LINENAME == linename).Count();
                        var cd2 = tatol.Where(u => u.reason1 == "遲到" && u.CLASS == CLASS && u.LINENAME == linename).Count();
                        var qj2 = tatol.Where(u => u.reason1 == "請假" && u.CLASS == CLASS && u.LINENAME == linename).Count();
                        var kg2 = tatol.Where(u => u.reason1 == "曠工" && u.CLASS == CLASS && u.LINENAME == linename).Count();
                        var zsb2 = tatol.Where(u => u.state2 == "中途上班" && u.CLASS == CLASS && u.LINENAME == linename).Count();
                        var zzt2 = tatol.Where(u => u.reason2 == "早退" && u.CLASS == CLASS && u.LINENAME == linename).Count();
                        var zqj2 = tatol.Where(u => u.reason2 == "請假" && u.CLASS == CLASS && u.LINENAME == linename).Count();
                        var zkg2 = tatol.Where(u => u.reason2 == "曠工" && u.CLASS == CLASS && u.LINENAME == linename).Count();
                        var time2 = tatol.Where(u => u.time1 != null && u.CLASS == CLASS && u.LINENAME == linename).Sum(u => u.time1);
                        BUdetail BUdetail2 = new BUdetail();

                        BUdetail2.BUID = linename; BUdetail2.CLASS = CLASS; BUdetail2.YD = YD2;
                        BUdetail2.SD = SD2; BUdetail2.cd = cd2; BUdetail2.qj = qj2; BUdetail2.kg = kg2;
                        BUdetail2.zsb = zsb2; BUdetail2.zzt = zzt2; BUdetail2.zqj = zqj2; BUdetail2.zkg = zkg2;
                        if (time2 != null) BUdetail2.time1 = double.Parse(time2.ToString());
                        if (YD2 != 0)
                        {
                            list_budetails.Add(BUdetail2);
                        }
                        BUdetail2.BUIDID = attendanceService.DbSession.LienNumberDal.FirstOrDefault(u => u.type1 == bu && u.linename == linename).id;
                    }

                }
                #endregion
            }
           // }
            #region   個部門的出勤情況

            ViewData["date_int"] = id1;
            ViewData["BU"] = bu;
            ViewData["Date"] = now.ToString("yyyy/MM/dd");
            ViewData["list_budetails"] = list_budetails;
            ViewData["list_budetail_total"] = list_budetail_total;
            #endregion
            return View();
        }

        //線長明細
        public ActionResult xzdetail2(string id)
        {
            List<xzdetail> list_xzdetail = new List<xzdetail>();
            if (id == null) id = "0";
            string[] uid = id.Split(',');
            string UserBU1 = "";
            string UserLINENAME1 = "";
            string UserCLASS1 = "";

            if (id == null) id = "0";
            if (uid.Count() == 4)
            {
                UserBU1 =uid[0];
                UserLINENAME1 = uid[1];
                UserCLASS1 = uid[2]=="b"?"白班":"晚班";

                id = uid[3];
               
            }
            int lineID = int.Parse(UserLINENAME1);
            UserLINENAME1 = attendanceService.DbSession.LienNumberDal.FirstOrDefault(u => u.id == lineID).linename;
            //查詢今日出勤明細 
            DateTime now = DateTime.Now.AddDays(int.Parse(id));
            string today = now.Date.ToShortDateString();
            DateTime start = Convert.ToDateTime(today.Trim() + " " + "00:00:00");
            DateTime end = Convert.ToDateTime(today.Trim() + " " + "23:59:59");

            //搜索
            if (Request.Form["RData"] != null && Request.Form["RData"].ToString().Length == 8)
            {
                if (common.IsDate(Request.Form["RData"].ToString()))
                {
                    string RData = Request.Form["RData"];
                    now = DateTime.ParseExact(RData, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    today = now.Date.ToShortDateString();
                    start = Convert.ToDateTime(today.Trim() + " " + "00:00:00");
                    end = Convert.ToDateTime(today.Trim() + " " + "23:59:59");
                    id = (now - DateTime.Now).Days.ToString();
                };
            }

            var temp = attendanceService.DbSession.AttendanceDal.GetEntity(u => u.BU == UserBU1 && u.LINENAME == UserLINENAME1 && u.CLASS == UserCLASS1 && u.date1 > start && u.date1 < end);
            foreach (Attendance ad in temp)
            {
                xzdetail dt = new xzdetail();
                dt.ID = ad.ID; dt.NAME = ad.NAME;
                if (ad.state1 == "到") { dt.cq = true; } else { dt.cq = false; }
                if (ad.reason1 == "遲到") { dt.cd = true; } else { dt.cd = false; }
                if (ad.reason1 == "請假") { dt.qj = true; } else { dt.qj = false; }
                if (ad.reason1 == "曠工") { dt.kg = true; } else { dt.kg = false; }
                if (ad.state2 == "中途上班") { dt.zsb = true; } else { dt.zsb = false; }
                if (ad.reason2 == "早退") { dt.zzt = true; } else { dt.zzt = false; }
                if (ad.reason2 == "請假") { dt.zqj = true; } else { dt.zqj = false; }
                if (ad.reason2 == "曠工") { dt.zkg = true; } else { dt.zkg = false; }
                if (ad.time1 != null) { dt.time1 = double.Parse(ad.time1.ToString());}
                list_xzdetail.Add(dt);
            }
            ViewData["list_xzdetail"] = list_xzdetail;
            ViewData["date_int"] = id;
            ViewData["Date"] = now.ToString("yyyy/MM/dd");
            ViewData["UserBU1"] = UserBU1;
            ViewData["UserLINENAME1"] = UserLINENAME1;
            ViewData["UserCLASS1"] = UserCLASS1;
            return View();
        }



        //線長點名可看的明細
        public ActionResult xzdetail(string id) 
        {
            List<xzdetail> list_xzdetail = new List<xzdetail>();
         
            //查詢今日出勤明細 
            if (id == null) id = "0";   
            DateTime now = DateTime.Now.AddDays(int.Parse(id));
            string today = now.Date.ToShortDateString();
            DateTime start = Convert.ToDateTime(today.Trim() + " " + "00:00:00");
            DateTime end = Convert.ToDateTime(today.Trim() + " " + "23:59:59");

            //搜索
            if (Request.Form["RData"] != null && Request.Form["RData"].ToString().Length==8)
            {
                if (common.IsDate(Request.Form["RData"].ToString()))
                {
                    string RData = Request.Form["RData"];
                    now = DateTime.ParseExact(RData, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    today = now.Date.ToShortDateString();
                    start = Convert.ToDateTime(today.Trim() + " " + "00:00:00");
                    end = Convert.ToDateTime(today.Trim() + " " + "23:59:59");
                };
               

            }

            var temp = attendanceService.DbSession.AttendanceDal.GetEntity(u => u.BU == UserBU && u.LINENAME == UserLINENAME && u.CLASS == UserCLASS &&u.date1>start&&u.date1<end);
            foreach (Attendance ad in temp)
            {
                xzdetail dt = new xzdetail();
                dt.ID = ad.ID; dt.NAME = ad.NAME;
                if (ad.state1 == "到") { dt.cq = true; } else { dt.cq = false; }
                if (ad.reason1 == "遲到") { dt.cd = true; } else { dt.cd = false; }
                if (ad.reason1 == "請假") { dt.qj = true; } else { dt.qj = false; }
                if (ad.reason1 == "曠工") { dt.kg = true; } else { dt.kg = false; }
                if (ad.state2 == "中途上班") { dt.zsb = true; } else { dt.zsb = false; }
                if (ad.reason2 == "早退") { dt.zzt = true; } else { dt.zzt = false; }
                if (ad.reason2 == "請假") { dt.zqj = true; } else { dt.zqj = false; }
                if (ad.reason2 == "曠工") { dt.zkg = true; } else { dt.zkg = false; }
                if (ad.time1 != null) { dt.time1 = double.Parse(ad.time1.ToString()); }
                list_xzdetail.Add(dt);
            }
            ViewData["list_xzdetail"] = list_xzdetail;
            ViewData["date_int"] = id;           
            ViewData["Date"] = now.ToString("yyyy/MM/dd");
            return View();
        }

        //缺勤明細
        public ActionResult qqdetail(string id)
        {
            
            List<qqdetail> list_qqdetail = new List<qqdetail>();
            if (id == null) id = "0";
            string[] uid = id.Split(',');
            string UserBU1 = "";
            
            if (id == null) id = "0";
            if (uid.Count() == 2)
            {
                UserBU1 = uid[0];       
                id = uid[1];
            }        
            //查詢今日出勤明細 
            DateTime now = DateTime.Now.AddDays(int.Parse(id));
            string today = now.Date.ToShortDateString();
            DateTime start = Convert.ToDateTime(today.Trim() + " " + "00:00:00");
            DateTime end = Convert.ToDateTime(today.Trim() + " " + "23:59:59");

            //搜索
            if (Request.Form["RData"] != null && Request.Form["RData"].ToString().Length == 8)
            {
                if (common.IsDate(Request.Form["RData"].ToString()))
                {
                    string RData = Request.Form["RData"];
                    now = DateTime.ParseExact(RData, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    today = now.Date.ToShortDateString();
                    start = Convert.ToDateTime(today.Trim() + " " + "00:00:00");
                    end = Convert.ToDateTime(today.Trim() + " " + "23:59:59");
                    id = (now - DateTime.Now).Days.ToString();
                };
            }
   
            var temp = attendanceService.DbSession.AttendanceDal.GetEntity(u => u.BU == UserBU1 && u.date1 > start && u.date1 < end && (u.state1 == "不到" || u.state2=="中途上班" || u.state2 == "中途離開")).OrderBy(u=>u.CLASS);
            foreach (Attendance ad in temp)
            {
                qqdetail dt = new qqdetail();
                dt.ID = ad.ID;
                dt.Name = ad.NAME;
                dt.Linename = ad.LINENAME;
                dt.Class = ad.CLASS;
                dt.reason = ad.reason1;
                if(ad.state2!="出勤" && ad.state2!="缺勤")
                { dt.ZTbd = ad.state2; }
                dt.ZTreason = ad.reason2;
                list_qqdetail.Add(dt);
            }
            ViewData["list_qqdetail"] = list_qqdetail;
            ViewData["date_int"] = id;
            ViewData["Date"] = now.ToString("yyyy/MM/dd");
            ViewData["bu"] = UserBU1;
            ViewData["UserLINENAME1"] = "";
            ViewData["UserCLASS1"] = "";
            return View();
        }


    }
}