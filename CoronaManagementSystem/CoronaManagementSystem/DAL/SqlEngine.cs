using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaManagementSystemHMO
{
    public static class SqlEngine
    {

        public static string connectionString = @"Data Source=.;Integrated Security=True;MultipleActiveResultSets=True;Initial Catalog=PatientDB;";

        public static int ExecuteNonQuery(string query, List<SqlParameter> sqlPara)
        {
            var response = 0;
            SqlConnection sqlConn = null;
            try
            {
                using (sqlConn = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        if (sqlPara != null)
                        {
                            sqlCmd.Parameters.AddRange(sqlPara.ToArray());
                        }
                        sqlConn.Open();
                        response = sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                sqlConn.Close();
                sqlConn.Dispose();
            }
            return response;
        }

        public static DataTable GetDataTable(string query, List<SqlParameter> sqlPara)
        {
            DataTable _dataTable = new DataTable();
            SqlConnection sqlConn = null;
            try
            {
                using (sqlConn = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        if (sqlPara != null)
                        {
                            sqlCmd.Parameters.AddRange(sqlPara.ToArray());
                        }
                        sqlConn.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(_dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                sqlConn.Close();
                sqlConn.Dispose();
            }
            return _dataTable;
        }
    }
}