using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.DAL
{
    public partial class Sys_UsersDal
    {
        DataModelContainer dbContext = new DataModelContainer();
        //更具賬號 密碼 查使用者
        public Sys_Users GetSys_Users(string Uid, string pwd)
        {
            var users = from a in dbContext.Sys_Users
                       where a.Emp_no == Uid && a.Password == pwd
                       select a;
            Sys_Users user = new Sys_Users();
            foreach (Sys_Users s in users)
            {
                user = s;
            }
            return user;

                        
        }
    }
}
