using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.DAL
{
    public partial class AttendanceDal : BaseDal<Attendance>, IAttendanceDal
    {
        DataModelContainer dbContext = new DataModelContainer();
      
      /// <summary>
      /// 批量添加 數據
      /// </summary>
      /// <param name="ID">工號數組</param>
      /// <param name="state">出勤狀態數組</param>
      /// <returns>添加數據的數量</returns>
        public int add_Attendance(List<string> ID, List<string> state)
        {
           
            int count = 0;
            foreach (string id in ID)
            {
               var temp= dbContext.Staff.FirstOrDefault(u => u.ID == id);
                {
                    Attendance a = new Attendance();
                    a.ID=  temp.ID;
                    a.NAME = temp.NAME;
                    a.BU = temp.BU;
                    a.LINENAME = temp.LINENAME;
                    a.CLASS = temp.CLASS;
                    a.state1 = state[count];
                    a.date1 = DateTime.Now;
                    //   a.date2 = null;a.note1 = null; a.note2 = null; a.reason2 = null;
                    dbContext.Attendance.Add(a);
                    //dbContext.SaveChanges();
                }
                count++;
            }
            return dbContext.SaveChanges();


        }

        public int update_Attendance(List<string> iD, List<string> state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 批量更改 數據
        /// </summary>
        /// <param name="ID">工號數組</param>
        /// <param name="state">出勤狀態數組</param>
        /// <param name="update">更新字段</param>
        /// <returns>更改數據的數量</returns>
        public int update_Attendance(List<string> ID, List<string> state,string update)
        {
            DateTime now = DateTime.Now;
            string today = now.Date.ToShortDateString();
            DateTime start = Convert.ToDateTime(today.Trim() + " " + "00:00:00");
            DateTime end = Convert.ToDateTime(today.Trim() + " " + "23:59:59");          
            int count = 0;
            foreach (string id in ID)
            {
                var temp = dbContext.Attendance.FirstOrDefault(u => u.ID == id&& u.date1 > start && u.date1 < end);
                {
                    string state1 = temp.state1;
                    string state2 = temp.state2;
                    Attendance newad = new Attendance();
                    newad = temp;
                    switch (update)
                    {
                        case "state1":
                         
                            newad.state1 = state[count]; newad.date1 = DateTime.Now;
                            if (state[count] != state1)
                            { 
                                newad.reason1 = null; newad.note1 = null; 
                                newad.reason2 = null; newad.state2 = null;
                            }
                            break;
                        case "reason1": newad.reason1 = state[count]; newad.date1 = DateTime.Now; break;
                        case "state2": newad.state2 = state[count]; newad.date2 = DateTime.Now;
                            if (state[count] != state2)
                            { 
                                newad.reason2 = null; newad.note2 = null;
                            }
                            break;
                        case "reason2": newad.reason2 = state[count]; newad.date2 = DateTime.Now; break;
                        case "time1": newad.time1 = double.Parse(state[count]);  break;
                    }
                   
                    dbContext.Entry(temp).CurrentValues.SetValues(newad); ;
                   
                }
                count++;
            }
            return dbContext.SaveChanges();


        }
        
    }
}
