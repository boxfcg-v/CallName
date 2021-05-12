using RollCall.DAL;
using RollCall.DALFactory;
using RollCall.IBLL;
using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.BLL
{
    public partial class WorkOvertimeService : BaseService<WorkOvertime>, IWorkOvertimeService
    {
       // DataModelContainer dbContext = new DataModelContainer();
        DbSession dbSession = new DbSession();
        public void addBatch(List<WorkOvertime> workOvertime)
        {
            foreach (WorkOvertime wot in workOvertime)
            {
                dbSession.WorkOvertimeDal.Add(wot);
            }
            dbSession.SaveChanges();
        }

        public void updateBatch(List<WorkOvertime> workOvertime)
        {
            foreach (WorkOvertime wot in workOvertime)
            {
               // var temp = dbSession.WorkOvertimeDal.GetEntity(u => u.ID == wot.ID && u.date1 == wot.date1);

                var temp = dbSession.WorkOvertimeDal.FirstOrDefault(u => u.ID == wot.ID && u.date1 == wot.date1);
                if (temp != null)
                {

                    temp.time1 = wot.time1;
                    temp.date2 = wot.date2;
                    //dbSession.WorkOvertimeDal.Update(wot);
                    // dbContext.Entry(temp).CurrentValues.SetValues(wot);
                }

                //dbContext.Entry(wot).State = EntityState.Modified;
                //dbContext.WorkOvertime.Attach(wot);
            }
            dbSession.SaveChanges();
        }

        
    }
}
