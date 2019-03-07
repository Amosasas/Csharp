using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data; //DataTable
using System.Data.SqlClient; //SqlConnection

namespace SCUT
{
    class DB : IDisposable
    {
        private SqlConnection sql_connection;
        private static DB db = null;
        private DB()
        {
            sql_connection = new SqlConnection("server=.\\SQLEXPRESS;" +
                                               "database=fushi_SCUT;" +
                                               "Trusted_Connection=SSPI;");
            sql_connection.Open();
        }

        public static DB getInstance()
        {
            if (db == null)
            {
                db = new DB();
            }
            return db;
        }
        public DataTable getBySql(string sql, Object[] param)
        {
            sql = String.Format(sql, param);
            SqlDataAdapter sql_data_adapter = new SqlDataAdapter(new SqlCommand(sql, sql_connection));
            DataTable data_table = new DataTable();
            sql_data_adapter.Fill(data_table);
            return data_table;

        }
        public DataTable getBySql(string sql)
        {
            SqlDataAdapter sql_data_adapter = new SqlDataAdapter(new SqlCommand(sql, sql_connection));
            DataTable data_table = new DataTable();
            sql_data_adapter.Fill(data_table);
            return data_table;
        }
        public void setBySql(string sql, Object[] param)
        {
            sql = String.Format(sql, param);

            Console.WriteLine(sql);

            new SqlCommand(sql, sql_connection).ExecuteNonQuery();
        }

        public void setBySql(string sql)
        {
            new SqlCommand(sql, sql_connection).ExecuteNonQuery();
        }

        public void Dispose()
        {
            sql_connection.Close();
        }
    }

    class Intent
    {
        public static Dictionary<string, Object> dict = new Dictionary<string, object>();
    }
}
