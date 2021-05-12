using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.IBLL
{
 
    public partial interface IAttendanceService:IBaseService<Attendance>
    {
    }
 
    public partial interface ILienNumberService:IBaseService<LienNumber>
    {
    }
 
    public partial interface IPhoneUsersService:IBaseService<PhoneUsers>
    {
    }
 
    public partial interface ISpecialCaseService:IBaseService<SpecialCase>
    {
    }
 
    public partial interface IStaffService:IBaseService<Staff>
    {
    }
 
    public partial interface IStaffXXXService:IBaseService<StaffXXX>
    {
    }
 
    public partial interface ISys_UsersService:IBaseService<Sys_Users>
    {
    }
 
    public partial interface IWorkOvertimeService:IBaseService<WorkOvertime>
    {
    }
}
