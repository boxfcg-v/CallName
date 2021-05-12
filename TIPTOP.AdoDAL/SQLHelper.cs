using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIPTOP.AdoDAL
{
    public class SQLHelper
    {
       // private static readonly string connString = ConfigurationManager.ConnectionStrings["Tiptop48"].ConnectionString;//獲取連接字符串
        private static readonly string connString = ConfigurationManager.AppSettings["Tiptop48"];
        public static DataTable GetTable(string sql, CommandType type, params OdbcParameter[] pars)
        {
            using (OdbcConnection conn = new OdbcConnection(connString))
            {
                using (OdbcDataAdapter apter = new OdbcDataAdapter(sql, conn))
                {
                    apter.SelectCommand.CommandType = type;
                    //if (pars != null)
                    //{
                    //    apter.SelectCommand.Parameters.AddRange(pars);
                    //}
                    DataTable da = new DataTable();
                    apter.Fill(da);
                    return da;
                }
            }
        }
        public static int ExecuteNonquery(string sql, CommandType type, params SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }

        }
        public static object ExecuteScalare(string sql, CommandType type, params SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

    }
}
