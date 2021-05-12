using RollCall.IDAL;
using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.DAL
{
  
    
   public partial interface IAttendanceDal:IBaseDal<Attendance>
    {
    }
  
    
   public partial interface ILienNumberDal:IBaseDal<LienNumber>
    {
    }
  
    
   public partial interface IPhoneUsersDal:IBaseDal<PhoneUsers>
    {
    }
  
    
   public partial interface ISpecialCaseDal:IBaseDal<SpecialCase>
    {
    }
  
    
   public partial interface IStaffDal:IBaseDal<Staff>
    {
    }
  
    
   public partial interface IStaffXXXDal:IBaseDal<StaffXXX>
    {
    }
  
    
   public partial interface ISys_UsersDal:IBaseDal<Sys_Users>
    {
    }
  
    
   public partial interface IWorkOvertimeDal:IBaseDal<WorkOvertime>
    {
    }
}

