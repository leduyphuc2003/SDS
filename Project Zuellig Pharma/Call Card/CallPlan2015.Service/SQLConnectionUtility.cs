using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CallPlan2015.Service
{
    public class SQLConnectionUtility
    {
        private SqlConnection m_connection = null;

        public SQLConnectionUtility()
        {
            m_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["UserLoginConnectionString"].ConnectionString);
        }

        public void OpenConnection()
        {
            if (m_connection.State != ConnectionState.Open) 
            {
                m_connection.Open();
            }
        }

        public string GetUser(string userCode, string passWord)
        {
            var result = new DataTable();
            string query = @"SELECT  *
                            FROM    UserCallPLan
                            WHERE   UserCode = @UserCode
                            AND     Password = @Password";

            var command = new SqlCommand(query, m_connection);
            var parameter = new SqlParameter("@UserCode", SqlDbType.NVarChar, 10);
            parameter.Value = userCode;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter("@Password", SqlDbType.NVarChar, 10);
            parameter.Value = passWord;
            command.Parameters.Add(parameter);

            var adapter = new SqlDataAdapter(command);

            try
            {
                adapter.Fill(result);
                if (result.Rows.Count > 0)
                {
                    return result.Rows[0]["UserCode"].ToString();
                }
            }
            
            finally
            {
                // Close connection
                CloseConnection();
            }

            return string.Empty;
        }

        public void CloseConnection()
        {
            if (m_connection.State == ConnectionState.Open)
            {
                m_connection.Close();
            }
        }

        public DataTable GetData(string query)
        {
            var result = new DataTable();
            
            var command = new SqlCommand(query, m_connection);
            var adapter = new SqlDataAdapter(command);
            OpenConnection();
            try
            {
                adapter.Fill(result);
            }

            finally
            {
                // Close connection
                CloseConnection();
            }

            return result;
        }

        public bool UpdateData(string update)
        {
            int updated = 0;
            var command = new SqlCommand(update, m_connection);
            OpenConnection();
            try
            {
                updated = command.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }

            return updated > 0;
        }
    }
}
