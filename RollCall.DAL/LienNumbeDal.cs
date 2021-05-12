using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.DAL
{
    public partial class LienNumbeDal
    {
        DataModelContainer dbContext = new DataModelContainer();
        public List<LienNumber> GetLienNumberBytype1(string BU)
        {
            List<LienNumber> lienNumber = new List<LienNumber>();
            //var temp = dbContext.Staff.Select(u => u.LINENAME == LineName);
            var temp = from s in dbContext.LienNumber
                       where s.type1 == BU
                       orderby s.linename
                       select s;
            if (BU == "PQ")
            {
                     temp = from s in dbContext.LienNumber
                           where s.type1.Contains("PQ") 
                           orderby s.linename
                           select s;
            }
            foreach (LienNumber sf in temp)
            {
                lienNumber.Add(sf);
            }
            return lienNumber;
        }
    }
}
