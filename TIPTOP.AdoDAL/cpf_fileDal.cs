using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIPTOP.Model;

namespace TIPTOP.AdoDAL
{
   public  class cpf_fileDal
    {
        public List<cpf_file> Getcpf_fileByIdBuDate(string ID,string BU,string RData)
        {
           
            //var temp = dbContext.Staff.Select(u => u.LINENAME == LineName);
            //string sql = "select cpf01,cpf02,cpf29,cpf70 from cpf_file where cpf01=@ID or cpf29=@BU or cpf70=@RData ";
            string  sql = "select cpf01,cpf02,cpf29,cpf70 from cpf_file where  cpf35 is null and  (cpf01='" + ID+"' or cpf29='"+BU+"' or cpf70='"+RData+"' )";

            OdbcParameter[] pars = {
                new OdbcParameter("@ID",OdbcType.Char),
                new OdbcParameter("@BU",OdbcType.Char),
                new OdbcParameter("@RData",OdbcType.DateTime),
            };
            pars[0].Value = ID;
            pars[1].Value = BU;
            pars[2].Value = RData;

            DataTable da = SQLHelper.GetTable(sql, CommandType.Text, pars);
            List<cpf_file> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<cpf_file>();
                cpf_file cpf_list = null;
                foreach (DataRow row in da.Rows)
                {
                    cpf_list = new cpf_file();
                    LoadEntity(row, cpf_list);
                    list.Add(cpf_list);

                }
            }
            
            return list;

        }


        public List<cpf_file> Getcpf_fileBylistId(List<string> uid)
        {


            StringBuilder sql = new StringBuilder();
            sql.Append("select cpf01,cpf02,cpf29,cpf70 from cpf_file where  cpf35 is null and ( 1!=1 ");

            foreach(string id in uid)
            {
                sql.Append(" or cpf01='" + id + "' ");
            }
            sql.Append(")");
            OdbcParameter[] pars = null;
            DataTable da = SQLHelper.GetTable(sql.ToString(), CommandType.Text, pars);
            List<cpf_file> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<cpf_file>();
                cpf_file cpf_list = null;
                foreach (DataRow row in da.Rows)
                {
                    cpf_list = new cpf_file();
                    LoadEntity(row, cpf_list);
                    list.Add(cpf_list);

                }
            }
            return list;

        }
        private void LoadEntity(DataRow row, cpf_file cpf_list)
        {
            cpf_list.cpf01 = row["cpf01"].ToString();
            cpf_list.cpf02 = row["cpf02"] != DBNull.Value ? row["cpf02"].ToString() : string.Empty;
            cpf_list.cpf29 = row["cpf29"] != DBNull.Value ? row["cpf29"].ToString() : string.Empty;
            cpf_list.cpf70 = DateTime.Parse(row["cpf70"].ToString());
            
        }
    }
}
