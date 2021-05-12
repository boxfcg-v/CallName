using RollCall.BLL;
using RollCall.DAL;
using RollCall.IBLL;
using RollCall.IDAL;
using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.BLL
{
 
   public partial class AttendanceService:BaseService<Attendance>,IAttendanceService
    {
		
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.AttendanceDal;
        }


	}
 
   public partial class LienNumberService:BaseService<LienNumber>,ILienNumberService
    {
		
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.LienNumberDal;
        }


	}
 
   public partial class PhoneUsersService:BaseService<PhoneUsers>,IPhoneUsersService
    {
		
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.PhoneUsersDal;
        }


	}
 
   public partial class SpecialCaseService:BaseService<SpecialCase>,ISpecialCaseService
    {
		
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.SpecialCaseDal;
        }


	}
 
   public partial class StaffService:BaseService<Staff>,IStaffService
    {
		
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.StaffDal;
        }


	}
 
   public partial class StaffXXXService:BaseService<StaffXXX>,IStaffXXXService
    {
		
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.StaffXXXDal;
        }


	}
 
   public partial class Sys_UsersService:BaseService<Sys_Users>,ISys_UsersService
    {
		
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.Sys_UsersDal;
        }


	}
 
   public partial class WorkOvertimeService:BaseService<WorkOvertime>,IWorkOvertimeService
    {
		
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.WorkOvertimeDal;
        }


	}
}
