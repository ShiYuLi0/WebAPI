using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebShiYuDB.Models
{
    public class SQLHelper
    {
        public static readonly string ConnectionString = ConfigurationManager.AppSettings["SQLConnStringMyself"];
        public static DataTable GetDataTable(string sql)
        {
            //使用using，在范围结束时处理对象，自动调用Dispose，但是实际上，后台一直存在，类似于电话一直保持通话状态。
            using (SqlConnection connection=new SqlConnection(ConnectionString))
            {
                using (SqlCommand command =new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
        public static int CommandSQL(string sql)
        {
            using (SqlConnection connection=new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command =new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    int count = command.ExecuteNonQuery();
                    return count;
                }
            }
        }

    }
}