using RollCall.BLL;
using RollCall.Model;
using RollCall.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RollCall.UI.Controllers
{
    public abstract class BaseController : Controller
    {
        // GET: Base
        public string BU { get; set; }

        public BaseController()
        {
            //  BU = "PE11";
            if (System.Web.HttpContext.Current.Session["BU"] != null)
            {
                BU = System.Web.HttpContext.Current.Session["BU"].ToString();
            }
            else
            {

                // HttpContext.Response.Redirect("/Home/Login");
                // return 
            }
            LienNumberService lienNumberService = new LienNumberService();
            List<LienNumber> lienNumbe = new List<LienNumber>();
            lienNumbe = lienNumberService.GetLienNumberBytype1(BU);
            if (BU != null)
            { 
                if (BU.Contains("PQ"))
                {
                    lienNumbe.Clear();
                    BU = "PQ";
                    lienNumbe = lienNumberService.GetLienNumberBytype1(BU);
                }
            }
            ViewData["LienNumber"] = lienNumbe;
            //換線 用的的線名與對應的id
            List<Department> lstRes = new List<Department>();
            int id = 1;
            foreach(LienNumber ln in lienNumbe)
            {
                lstRes.Add(new Department() { ID = id.ToString(), Name = ln.linename });
                id++;
            }
            ViewData["Department"] = lstRes;


        }
    }
}