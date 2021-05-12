using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RollCall.UI
{
    public static class HMTLHelperExtensions
    {
        public static string isActive(this HtmlHelper html, string controller = null, string action = null,string id=null)
        {
            string activeClass = "active"; // change here if you another name to activate sidebar items
            // detect current app state
            string actualAction = (string)html.ViewContext.RouteData.Values["action"];
            string actualController = (string)html.ViewContext.RouteData.Values["controller"];
            string actualid = (string)html.ViewContext.RouteData.Values["ID"];
            if (String.IsNullOrEmpty(controller))
                controller = actualController;

            if (String.IsNullOrEmpty(action))
                action = actualAction;

            if (String.IsNullOrEmpty(id))
                id = actualid;
            //&& id == actualid
            return (controller == actualController && action == actualAction && id ==actualid ) ? activeClass : String.Empty;
        }
    }
}