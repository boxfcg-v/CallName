using RollCall.DAL;
using RollCall.DALFactory;
using RollCall.IBLL;
using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.BLL
{
    public partial class AttendanceService:BaseService<Attendance>, IAttendanceService
    {
        IAttendanceDal Dal= new AttendanceDal();
        DbSession dbSession = new DbSession();
        //public int add_Attendance(List<string> ID, List<string> state)
        //{
        //    dbSession.AttendanceDal.Add.
        //    //return Dal.add_Attendance(ID, state);

        //}
        public void add_Attendance(List<Attendance> attendance)
        {
            foreach (Attendance at in attendance)
            {
                dbSession.AttendanceDal.Add(at);
                //try
                //{
                //    dbSession.SaveChanges();
                //}
                //catch (DbEntityValidationException ex)
                //{
                //    StringBuilder errors = new StringBuilder();
                //    IEnumerable<DbEntityValidationResult> validationResult = ex.EntityValidationErrors;
                //    foreach (DbEntityValidationResult result in validationResult)
                //    {
                //        ICollection<DbValidationError> validationError = result.ValidationErrors;
                //        foreach (DbValidationError err in validationError)
                //        {
                //            errors.Append(err.PropertyName + ":" + err.ErrorMessage + "\r\n");
                //        }
                //    }
                //    Console.WriteLine(errors.ToString());
                    
                //}
                

            }
            dbSession.SaveChanges();
        }

        public int update_Attendance(List<string> ID, List<string> state, string update)
        {

            return Dal.update_Attendance(ID, state, update);

        }

        //public void update_Attendance(List<Attendance> attendance)
        //{
        //    DateTime now = DateTime.Now;
        //    string today = now.Date.ToShortDateString();
        //    DateTime start = Convert.ToDateTime(today.Trim() + " " + "00:00:00");
        //    DateTime end = Convert.ToDateTime(today.Trim() + " " + "23:59:59");
        //    int count = 0;
        //    foreach (string id in ID)
        //    {
        //        var temp = dbContext.Attendance.FirstOrDefault(u => u.ID == id && u.date1 > start && u.date1 < end);
        //        {
        //            string state1 = temp.state1;
        //            string state2 = temp.state2;
        //            Attendance newad = new Attendance();
        //            newad = temp;
        //            switch (update)
        //            {
        //                case "state1":

        //                    newad.state1 = state[count]; newad.date1 = DateTime.Now;
        //                    if (state[count] != state1)
        //                    {
        //                        newad.reason1 = null; newad.note1 = null;
        //                        newad.reason2 = null; newad.state2 = null;
        //                    }
        //                    break;
        //                case "reason1": newad.reason1 = state[count]; newad.date1 = DateTime.Now; break;
        //                case "state2":
        //                    newad.state2 = state[count]; newad.date2 = DateTime.Now;
        //                    if (state[count] != state2)
        //                    {
        //                        newad.reason2 = null; newad.note2 = null;
        //                    }
        //                    break;
        //                case "reason2": newad.reason2 = state[count]; newad.date2 = DateTime.Now; break;
        //                case "time1": newad.time1 = double.Parse(state[count]); break;
        //            }

        //            dbContext.Entry(temp).CurrentValues.SetValues(newad); ;

        //        }
        //        count++;
        //    }
        //    return dbContext.SaveChanges();


        //}

    }
}
