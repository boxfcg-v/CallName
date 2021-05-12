using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.IDAL
{
    public partial interface IDbSession
    {
        #region 改成TT模板自动生成
        // IStaffDal StaffDal { get; }
        #endregion
        int SaveChanges();
    }
}
