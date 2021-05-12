using RollCall.DAL;
using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.BLL
{
    public partial  class PhoneUsersService
    {

        PhoneUsersDal Dal = new PhoneUsersDal();

        public int addPhoneUsersList(List<PhoneUsers> phoneUsers)
        {

            return Dal.addPhoneUsersList(phoneUsers);
        }

        public int delPhoneUsers(List<string > id)
        {

            return Dal.delPhoneUsers(id);
        }
        

    }
}
