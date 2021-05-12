using RollCall.DAL;
using RollCall.IBLL;
using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RollCall.IDAL;
using System.Linq.Expressions;

namespace RollCall.BLL
{
    public partial  class StaffService : BaseService<Staff>,IStaffService
    {
        StaffDal staffDal = new StaffDal();

        ////依赖接口编程
        ////IStaffDal staffDal = new StaffDal();

        //IStaffDal staffDal = StaticDalFactory.GetStaffDal();

        ////IDbSession dbSession = new DbSession(); 一次请求公用一个实例
        //IDbSession dbSession = DbSessionFactory.GetCurrentDbSession();
        ////public Staff Add(Staff staff)
        ////{
        ////    //return staffDal.Add(staff);
        ////    dbSession.StaffDal.Add(staff);


        ////    dbSession.SaveChanges();//数据提交的权利从数据库访问层提到了业务逻辑层
        ////    return dbSession.StaffDal.Add(staff);
        ////}


        //public StaffService(IDbSession dbSession) : base(dbSession)
        //{
        //    this.DbSession = dbSession;
        //}
      
        public List<Staff> GetStaffByLineName(string LineName,string BU)
        {
            return staffDal.GetStaffByLineName(LineName,BU);
        }
        public List<Staff> GetStaffAll(string BU)
        {
            return staffDal.GetStaffAll(BU);
        }
       public int addStaffList(List<Staff> staff)
        {
            
            return staffDal.addStaffList(staff);
        }

        public List<Staff> GetStaffAll()
        {
            return staffDal.GetStaffAll();
        }

        public int delStaff(List<string> ID)
        {
           
            return staffDal.delStaff(ID);

        }

        public int ChangLinenameStaff(string ID, string linename)
        {


            //dbContext.Staff
            return staffDal.ChangLinenameStaff(ID, linename);
        }

        public int UpdateClass(List<string> uuid)
        {
            return staffDal.UpdateClass(uuid);
        }

        public List<Staff> GetStaffLineBoss(string bu)
        {
            return staffDal.GetStaffLineBoss(bu);
        }

        public IQueryable<Staff> GetPageEntitys<S>(int pageSize, int pageIndex, out int total, Expression<Func<Staff, bool>> whereLambda, Expression<Func<Staff, S>> orderByLambda, bool isAse)
        {
            throw new NotImplementedException();
        }
    }
}
