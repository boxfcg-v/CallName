using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RollCall.DAL;

namespace RollCall.IDAL
{
    public partial interface IDbSession
    {
  
    
    IAttendanceDal AttendanceDal {get;}

  
    
    ILienNumberDal LienNumberDal {get;}

  
    
    IPhoneUsersDal PhoneUsersDal {get;}

  
    
    ISpecialCaseDal SpecialCaseDal {get;}

  
    
    IStaffDal StaffDal {get;}

  
    
    IStaffXXXDal StaffXXXDal {get;}

  
    
    ISys_UsersDal Sys_UsersDal {get;}

  
    
    IWorkOvertimeDal WorkOvertimeDal {get;}

	}
}

