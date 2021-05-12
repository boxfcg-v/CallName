using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.DAL
{
    public partial class PhoneUsersDal
    {
        DataModelContainer dbContext = new DataModelContainer();
        public int addPhoneUsersList(List<PhoneUsers> phoneUsers)
        {
            try
            {
                foreach (PhoneUsers a in phoneUsers)
                {
                    dbContext.PhoneUsers.Add(a);
                }
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {

                return 0;
            }

        }

        public int delPhoneUsers(List<string> ID)
        {
            if (ID == null) return 0;
 
            foreach (string id in ID)
            {
                var delPhoneUsers = dbContext.PhoneUsers.FirstOrDefault(m => m.ID == id);
               
                if (delPhoneUsers != null)
                {
                    dbContext.PhoneUsers.Remove(delPhoneUsers);
                  
                }
            }
            return dbContext.SaveChanges(); ;

        }
    }
}
