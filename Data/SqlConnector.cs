using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SqlConnector
    {
        public static DataTable ReadFromSP(string connectionString, string spName, params SqlParameter[] sqlParameters)
        {
            SqlConnection conn = null;
            SqlDataReader sqlDataReader = null;
            DataTable dataTable = new DataTable();

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(spName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (sqlParameters != null)
                {
                    foreach (var item in sqlParameters)
                    {
                        cmd.Parameters.Add(item);
                    }
                }
                sqlDataReader = cmd.ExecuteReader();
                dataTable.Load(sqlDataReader);
                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                    conn.Close();

                if (sqlDataReader != null)
                    sqlDataReader.Close();
            }
            return dataTable;
        }
    }
}
