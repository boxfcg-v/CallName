using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RollCall.UI.Controllers
{
    public class BasePhoneController : Controller
    {
        // GET: BasePhone
        
        public string UserID { get { try { return Session["UserID"].ToString(); } catch { RedirectToAction("../Home/Login"); return ""; } } set { } }
        public string UserBU { get { return Session["UserBU"].ToString(); } set { } }
        public string UserCLASS { get { return Session["UserCLASS"].ToString(); } set { Session["UserCLASS"] = value; } }
        public string UserLINENAME { get { return Session["UserLINENAME"].ToString(); } set { } }
    }
    
     
}