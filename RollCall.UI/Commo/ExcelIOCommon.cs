using RollCall.DALFactory;
using RollCall.IDAL;
using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RollCall.UI.Commo
{
    public class ExcelIOCommon
    {
        IDbSession dbSession = new DbSession();
        public void addStaff(DataTable dt )
        {
            //deleteStaff(dt);
            foreach (DataRow dr in dt.Rows)
            {
                
                string id = dr.Field<string>("工號").ToString().Trim();
                string BU = dr.Field<string>("部門").ToString().Trim();
                string CLASS = dr.Field<string>("班別").ToString().Trim();
                string LINENAME = dr.Field<string>("線別").ToString().Trim();
                string POSITION = dr.Field<string>("職位").ToString().Trim();
                var temp = dbSession.StaffDal.FirstOrDefault(u => u.ID == id);
                if (temp != null)
                {
                    //if (temp.BU == BU && temp.CLASS == CLASS && temp.LINENAME == LINENAME && temp.POSITION == POSITION)
                    //{
                    //    continue;
                    //}
                    dbSession.StaffDal.Delete(temp);
                }
                Staff sf = new Staff();
                sf.ID = dr.Field<string>("工號").ToString().Trim();
                sf.NAME = dr.Field<string>("姓名").ToString().Trim();
                sf.POSITION = dr.Field<string>("職位").ToString().Trim();
                sf.LINENAME = dr.Field<string>("線別").ToString().Trim();
                sf.CLASS = dr.Field<string>("班別").ToString().Trim();
                sf.date1 = DateTime.Parse(dr.Field<string>("進廠日期").ToString());
                sf.date2 = DateTime.Now;
                sf.BU = dr.Field<string>("部門").ToString().Trim(); 
                sf.BUID = dr.Field<string>("部門").ToString().Trim();
                dbSession.StaffDal.Add(sf);
                dbSession.SaveChanges();
            }
            

        }

        public void deleteStaff(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string  id=dr.Field<string>("工號").ToString().Trim();
                var temp = dbSession.StaffDal.FirstOrDefault(u => u.ID == id);
                if (temp != null)
                {
                    dbSession.StaffDal.Delete(temp);
                }
            }
            dbSession.SaveChanges();

        }
        public void addLienNumber(DataTable dt)
        {
           string bu= dt.Rows[0]["部門"].ToString();
            string orby =  dbSession.LienNumberDal.FirstOrDefault(u => u.type1 == bu).OrderbBy.ToString();
            int orbyi = int.Parse(orby);
            var temp = (from a in dt.AsEnumerable()
                   
                        select new
                        {
                            bu = a.Field<string>("部門"),
                            lnn = a.Field<string>("線別")
                        });
          var temp1 = (from a in temp.AsEnumerable()
                       group a by new { a.bu, a.lnn} into b
                       select new
                       {
                           bu = b.Key.bu,
                           lnn = b.Key.lnn
                       });
           
            int aa=temp1.Count();
            foreach (var dr in temp1)
            {
                var te = dbSession.LienNumberDal.FirstOrDefault(u => u.type1 == dr.bu && u.linename == dr.lnn);
                if(te==null)
                { 
                    LienNumber ln = new LienNumber();
                    ln.type1 = dr.bu;
                    ln.linename = dr.lnn;
                    ln.OrderbBy = orbyi;
                    dbSession.LienNumberDal.Add(ln);
                }
            }
            dbSession.SaveChanges();
        }

        public void addPhoneUsers(DataTable dt)
        {
            //deletePhoneUsers(dt);
            
            foreach (DataRow dr in dt.Rows)
            {
                #region  
                //string id = dr.Field<string>("工號").ToString().Trim();
                //var tem = dbSession.PhoneUsersDal.FirstOrDefault(u => u.ID == id);
                //if (tem == null)
                //{
                //    if (dr.Field<string>("職位").ToString() == "線長")
                //    {
                //        PhoneUsers pu = new PhoneUsers();
                //        pu.ID = dr.Field<string>("工號").ToString().Trim();
                //        pu.PASSWORD1 = dr.Field<string>("工號").ToString().Trim();
                //        pu.power1 = "1";
                //        dbSession.PhoneUsersDal.Add(pu);
                //    }
                //}
                #endregion

                
                
                if (dr.Field<string>("職位").ToString() == "線長")
                {
                    string id = dr.Field<string>("工號").ToString().Trim();
                    var tem = dbSession.PhoneUsersDal.FirstOrDefault(u => u.ID == id);
                    if (tem == null)
                    {
                        PhoneUsers pu = new PhoneUsers();
                        pu.ID = dr.Field<string>("工號").ToString().Trim();
                        pu.PASSWORD1 = dr.Field<string>("工號").ToString().Trim();
                        pu.power1 = "1";
                        dbSession.PhoneUsersDal.Add(pu);
                    }
                    else
                    {
                        if (tem.power1 != "1")
                            tem.power1 = "1";
                    }
                }
                if (dr.Field<string>("職位").ToString() == "主管")
                {
                    string id = dr.Field<string>("工號").ToString().Trim();
                    var tem = dbSession.PhoneUsersDal.FirstOrDefault(u => u.ID == id);
                    if (tem == null)
                    {
                        PhoneUsers pu = new PhoneUsers();
                        pu.ID = dr.Field<string>("工號").ToString().Trim();
                        pu.PASSWORD1 = dr.Field<string>("工號").ToString().Trim();
                        pu.power1 = "3";
                        dbSession.PhoneUsersDal.Add(pu);
                    }
                    else
                    {
                        if (tem.power1 != "3")
                            tem.power1 = "3";
                    }
                }

            }
            dbSession.SaveChanges();
        }

        public void deletePhoneUsers(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.Field<string>("職位").ToString() == "線長")
                {
                    string id = dr.Field<string>("工號").ToString().Trim();
                    var tem = dbSession.PhoneUsersDal.FirstOrDefault(u => u.ID == id);
                    if(tem==null)
                    {
                        var temp = dbSession.PhoneUsersDal.FirstOrDefault(u => u.ID == id);
                        if (temp != null)
                        {
                            dbSession.PhoneUsersDal.Delete(temp);
                        }
                    }
                }
            }
            dbSession.SaveChanges();

        }


    }
}