using RollCall.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.DALFactory
{
   public partial class StaticDalFactory
   {
        //public static string DalAssemblyName = ConfigurationManager.AppSettings["DalAssemblyName"];
        public static string assemblyName = ConfigurationManager.AppSettings["DalAssemblyName"];
        //public static IStaffDal GetStaffDal()
        //{
        //    //简单工厂
        //    //return new StaffDal();


        //    //抽象工厂
        //    //把上面的new 
        //    //return Assembly.Load("MVC.RollCall.EFDAL").CreateInstance
        //    //    ("MVC.RollCall.EFDAL.StaffDal") 
        //    //    as StaffDal;

        //    //去掉希望改一个配置创建实例就发生变化，不需要改代码

        //    return Assembly.Load(DalAssemblyName).CreateInstance
        //        (DalAssemblyName + ".StaffDal")
        //        as StaffDal;

        //}

        #region 有TT模板自动生成


        //public static IStaffDal StaffDal
        //{
        //    get { return new StaffDal(); }
        //}

        #endregion

    }
}
