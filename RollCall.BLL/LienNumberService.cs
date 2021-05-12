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
   public partial class LienNumberService:BaseService<LienNumber>
    {
        LienNumbeDal lienNumberDal = new LienNumbeDal();
        public List<LienNumber> GetLienNumberBytype1(string BU)
        {
            return lienNumberDal.GetLienNumberBytype1(BU);
        }

    }
}
