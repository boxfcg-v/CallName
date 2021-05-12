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
	 

        public static IAttendanceDal GetAttendanceDal
        {
			get
			{
				 return Assembly.Load(assemblyName).CreateInstance
                (assemblyName+".AttendanceDal") 
                as AttendanceDal;
			}
			  // get { return new AttendanceDal(); }
        }
	 

        public static ILienNumberDal GetLienNumberDal
        {
			get
			{
				 return Assembly.Load(assemblyName).CreateInstance
                (assemblyName+".LienNumberDal") 
                as LienNumberDal;
			}
			  // get { return new LienNumberDal(); }
        }
	 

        public static IPhoneUsersDal GetPhoneUsersDal
        {
			get
			{
				 return Assembly.Load(assemblyName).CreateInstance
                (assemblyName+".PhoneUsersDal") 
                as PhoneUsersDal;
			}
			  // get { return new PhoneUsersDal(); }
        }
	 

        public static ISpecialCaseDal GetSpecialCaseDal
        {
			get
			{
				 return Assembly.Load(assemblyName).CreateInstance
                (assemblyName+".SpecialCaseDal") 
                as SpecialCaseDal;
			}
			  // get { return new SpecialCaseDal(); }
        }
	 

        public static IStaffDal GetStaffDal
        {
			get
			{
				 return Assembly.Load(assemblyName).CreateInstance
                (assemblyName+".StaffDal") 
                as StaffDal;
			}
			  // get { return new StaffDal(); }
        }
	 

        public static IStaffXXXDal GetStaffXXXDal
        {
			get
			{
				 return Assembly.Load(assemblyName).CreateInstance
                (assemblyName+".StaffXXXDal") 
                as StaffXXXDal;
			}
			  // get { return new StaffXXXDal(); }
        }
	 

        public static ISys_UsersDal GetSys_UsersDal
        {
			get
			{
				 return Assembly.Load(assemblyName).CreateInstance
                (assemblyName+".Sys_UsersDal") 
                as Sys_UsersDal;
			}
			  // get { return new Sys_UsersDal(); }
        }
	 

        public static IWorkOvertimeDal GetWorkOvertimeDal
        {
			get
			{
				 return Assembly.Load(assemblyName).CreateInstance
                (assemblyName+".WorkOvertimeDal") 
                as WorkOvertimeDal;
			}
			  // get { return new WorkOvertimeDal(); }
        }
		}
}

