using RollCall.DAL;
using RollCall.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.DALFactory
{
    public partial class DbSession : IDbSession
    {
  
    
    public IAttendanceDal AttendanceDal
    {
         get { return StaticDalFactory.GetAttendanceDal; }
		 //get { return new AttendanceDal(); }
    }
    
    
  
    
    public ILienNumberDal LienNumberDal
    {
         get { return StaticDalFactory.GetLienNumberDal; }
		 //get { return new LienNumberDal(); }
    }
    
    
  
    
    public IPhoneUsersDal PhoneUsersDal
    {
         get { return StaticDalFactory.GetPhoneUsersDal; }
		 //get { return new PhoneUsersDal(); }
    }
    
    
  
    
    public ISpecialCaseDal SpecialCaseDal
    {
         get { return StaticDalFactory.GetSpecialCaseDal; }
		 //get { return new SpecialCaseDal(); }
    }
    
    
  
    
    public IStaffDal StaffDal
    {
         get { return StaticDalFactory.GetStaffDal; }
		 //get { return new StaffDal(); }
    }
    
    
  
    
    public IStaffXXXDal StaffXXXDal
    {
         get { return StaticDalFactory.GetStaffXXXDal; }
		 //get { return new StaffXXXDal(); }
    }
    
    
  
    
    public ISys_UsersDal Sys_UsersDal
    {
         get { return StaticDalFactory.GetSys_UsersDal; }
		 //get { return new Sys_UsersDal(); }
    }
    
    
  
    
    public IWorkOvertimeDal WorkOvertimeDal
    {
         get { return StaticDalFactory.GetWorkOvertimeDal; }
		 //get { return new WorkOvertimeDal(); }
    }
    
    
	}
}

