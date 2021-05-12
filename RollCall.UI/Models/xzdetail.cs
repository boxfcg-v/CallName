using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RollCall.UI.Models
{
    public class xzdetail
    {
        public string ID { get; set; }
        public string NAME { get; set; }

        public bool cq { get; set; }//出勤

        public bool cd { get; set; }//遲到

        public bool qj { get; set; }//請假
        public bool kg { get; set; }//曠工



        public bool zsb { get; set; }//中途上班
        public bool zzt { get; set; }//中途早退
        public bool zqj { get; set; }//中途請假
        public bool zkg { get; set; }//中途曠工

        public double time1 { get; set; }//預報加班
         
    }
}