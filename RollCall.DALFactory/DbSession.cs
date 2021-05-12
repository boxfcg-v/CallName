using RollCall.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.DALFactory
{
    public partial class DbSession
    {

        #region 简单工厂或者抽象工厂 有TT模板自动生成
        //public IStaffDal StaffDal
        //{
        //    get { return StaticDalFactory.GetStaffDal(); }
        //}
        #endregion


        public int SaveChanges()
        {

            return DbContextFactory.GetCurrentDbContext().SaveChanges();
        }
    }
}
