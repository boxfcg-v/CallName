using RollCall.IDAL;
using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.DAL
{
    public partial interface IAttendanceDal : IBaseDal<Attendance>
    {
        int add_Attendance(List<string> iD, List<string> state);
        int update_Attendance(List<string> iD, List<string> state, string update);
    }
}
