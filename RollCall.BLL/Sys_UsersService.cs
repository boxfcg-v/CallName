using RollCall.DAL;
using RollCall.IBLL;
using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.BLL
{
    public partial class Sys_UsersService:BaseService<Sys_Users>
    {
        Sys_UsersDal Dal = new Sys_UsersDal();
        public Sys_Users GetSys_Users(string Uid, string pwd)
        {
            return Dal.GetSys_Users(Uid, pwd);
        }

      
    }
}
