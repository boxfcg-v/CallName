using RollCall.DAL;
using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.DAL
{
 
    public partial class AttendanceDal: BaseDal<Attendance>,IAttendanceDal 
    {
	}
    

 
    public partial class LienNumberDal: BaseDal<LienNumber>,ILienNumberDal 
    {
	}
    

 
    public partial class PhoneUsersDal: BaseDal<PhoneUsers>,IPhoneUsersDal 
    {
	}
    

 
    public partial class SpecialCaseDal: BaseDal<SpecialCase>,ISpecialCaseDal 
    {
	}
    

 
    public partial class StaffDal: BaseDal<Staff>,IStaffDal 
    {
	}
    

 
    public partial class StaffXXXDal: BaseDal<StaffXXX>,IStaffXXXDal 
    {
	}
    

 
    public partial class Sys_UsersDal: BaseDal<Sys_Users>,ISys_UsersDal 
    {
	}
    

 
    public partial class WorkOvertimeDal: BaseDal<WorkOvertime>,IWorkOvertimeDal 
    {
	}
    

}

