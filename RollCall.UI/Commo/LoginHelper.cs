using RollCall.BLL;
using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RollCall.UI.Commo
{
    public class LoginHelper
    {
        Sys_UsersService Sys_User = new Sys_UsersService();
        //判斷 賬號 密碼 并保存Session
        public bool CheckUidAndPwd(string Uid, string Pwd)
        {
            bool judge = false;
         
            Sys_Users user = new Sys_Users();
            user =Sys_User.GetSys_Users(Uid, Pwd);
            if (user != null)
            {
                judge = true;
               
               
            }

            return judge;
        }
    }
}