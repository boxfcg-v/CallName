using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RollCall.UI.App_Start
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判断Cookie用户名密码是否存在  
           // HttpCookie cookieName = System.Web.HttpContext.Current.Request.Cookies.Get("name");
            if (System.Web.HttpContext.Current.Session["BU"] == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}