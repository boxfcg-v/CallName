using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIPTOP.Model;

namespace RollCall.UI.Commo
{
   
    public class Commoo
    {

        DataModelContainer dbContext = new DataModelContainer();

        //將查詢的cpf 裝換成Staff 數據  部門
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpf">查詢的cpf數組</param>
        /// <param name="BU">部門</param>
        /// <param name="cls">白晚班</param>
        /// <param name="ln">線名</param>
        /// <param name="ps">職位</param>
        /// <returns></returns>
        public List<Staff> GetStaffList(List<cpf_file> cpf,string BU,string cls,string ln,string ps)
        {
            List<Staff> staff = new List<Staff>();
            foreach (cpf_file c in cpf)
            {
                Staff s = new Staff();
                s.ID = c.cpf01;
                s.NAME = c.cpf02;
                s.BUID = c.cpf29;
                s.date1 = c.cpf70;
                //s.BU = BU;
                s.BU = c.cpf29;
                s.CLASS = cls;
                s.LINENAME = ln;
                s.POSITION = ps;
                s.date2 = DateTime.Now;
                staff.Add(s);
               
            }
            return staff;


        }



        //添加線長手機賬號
        public List<PhoneUsers> GetPhoneUsersList(List<cpf_file> cpf)
        {
            List<PhoneUsers> phoneUsers = new List<PhoneUsers>();
            foreach (cpf_file c in cpf)
            {
                PhoneUsers s = new PhoneUsers();
                s.ID = c.cpf01;
                s.PASSWORD1 = c.cpf01;
                s.power1 = "1";

                phoneUsers.Add(s);

            }
            return phoneUsers;

        }







        //過濾已存在的ID數據
        public List<cpf_file> filtercpf(List<cpf_file> cpf)
        {
            var staff = from s in dbContext.Staff
                        select s.ID;

            //var  ck  = 
            var temp = from a in cpf
                       where !staff.Contains(a.cpf01)
                       select a;

            List<cpf_file> newcpf = new List<cpf_file>();
            foreach (cpf_file c in temp)
            {
                newcpf.Add(c);
            }
            return newcpf;
        }
    }
}