using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.DAL
{
    public partial class StaffDal : BaseDal<Staff>,IStaffDal
    {
        //更具線名抓取Staff員工數據
        DataModelContainer dbContext = new DataModelContainer();
        public List<Staff> GetStaffByLineName(string LineName,string BU)
        {
            List<Staff> staff = new List<Staff>() ;
            //var temp = dbContext.Staff.Select(u => u.LINENAME == LineName);
            var temp = from s in dbContext.Staff
                       where s.LINENAME == LineName && s.BU==BU
                       select s;
            if (BU.Contains("PQ"))
            {
                 temp = from s in dbContext.Staff
                        where s.LINENAME == LineName && s.BU.Contains("PQ")
                        select s;
            }

            foreach (Staff sf in temp)
            {
                staff.Add(sf);
            }
            return staff;
                       
        }

        public List<Staff> GetStaffAll()
        {
            List<Staff> staff = new List<Staff>();
            //var temp = dbContext.Staff.Select(u => u.LINENAME == LineName);
            var temp = from s in dbContext.Staff
                       select s;
            foreach (Staff sf in temp)
            {
                staff.Add(sf);
            }
            return staff;
        }

        public List<Staff> GetStaffAll(string BU)
        {
            List<Staff> staff = new List<Staff>();
            //var temp = dbContext.Staff.Select(u => u.LINENAME == LineName);
            var temp = from s in dbContext.Staff
                       where s.BU == BU 
                       select s;
            foreach (Staff sf in temp)
            {
                staff.Add(sf);
            }
            return staff;

        }

        public List<Staff> GetStaffLineBoss(string BU)
        {
            List<Staff> staff = new List<Staff>();
            //var temp = dbContext.Staff.Select(u => u.LINENAME == LineName);
            var temp = from s in dbContext.Staff
                       where  s.BU == BU && s.POSITION=="線長"
                       select s;
            if (BU.Contains("PQ"))
            {
                 temp = from s in dbContext.Staff
                        where s.BU.Contains("PQ") && s.POSITION == "線長"
                        select s;
            }
            foreach (Staff sf in temp)
            {
                staff.Add(sf);
            }
            return staff;
        }

        public int UpdateClass(List<string> ID)
        {
            if (ID == null) return 0;

            foreach (string id in ID)
            {
                //需要修改的實體
                var staff = dbContext.Staff.FirstOrDefault(m => m.ID == id);
                if (staff != null)
                {
                 
                    Staff newstaff = new Staff();
                    newstaff = staff;
                    newstaff.CLASS = staff.CLASS == "白班" ? "晚班" : "白班";  //換班
                    dbContext.Entry(staff).CurrentValues.SetValues(newstaff);

                }
            }
            return dbContext.SaveChanges();
        }

        public int addStaffList(List<Staff> staff)
        {
            try
            {
                foreach (Staff s in staff)
                {
                    dbContext.Staff.Add(s);
                }
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {

                return 0;
            }

           
        }

        public int delStaff(List<string> ID)
        {
            if (ID == null) return 0;
                
            foreach(string id in ID)
            {
                var delstaff = dbContext.Staff.FirstOrDefault(m => m.ID == id);
                if (delstaff != null)
                { 
                    dbContext.Staff.Remove(delstaff);
                }
            }
            return dbContext.SaveChanges();

        }
        //更具ID 換線名
        public int ChangLinenameStaff(string ID, string linename)
        {
            //更具ID查詢人員
            var staff = dbContext.Staff.FirstOrDefault(m => m.ID == ID);
            staff.LINENAME = linename;

            //dbContext.Staff
           return dbContext.SaveChanges();
        }
            

     }


}

