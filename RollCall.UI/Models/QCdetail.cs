using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RollCall.UI.Models
{
    public class QCdetail
    {
        //部門 編制 實際 出勤 遲到 請假 曠工 中途上班 早退 請假 曠工 預報加班
        public string BUID { get; set; }
        public int BZ { get; set; }
        public int SJ { get; set; }

        public int cq { get; set; }//出勤

        public int cd{ get; set; }//遲到

        public int qj { get; set; }//請假
        public int kg { get; set; }//曠工



        public int zsb { get; set; }//中途上班
        public int zzt { get; set; }//中途早退
        public int zqj { get; set; }//中途請假
        public int zkg { get; set; }//中途曠工

        public double time1 { get; set; }//預報加班
    }
}