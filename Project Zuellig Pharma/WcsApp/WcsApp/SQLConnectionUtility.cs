using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcsApp
{
    public class SQLConnectionUtility
    {
        private SqlConnection m_connection = null;

        public SQLConnectionUtility()
        {
            //string conec = "Data Source=VNZPDPLE22;Initial Catalog=EmployeeSurvey;Integrated Security=true";
            //m_connection = new SqlConnection(conec);

            m_connection = new SqlConnection();
            m_connection.ConnectionString= "Data Source=VNZPDPLE22;Initial Catalog=ttt;Integrated Security=true";
        }

        public void OpenConnection()
        {
            if (m_connection.State != ConnectionState.Open)
            {
                m_connection.Open();
            }
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
            //command.CommandTimeout = TimeSpan.FromMinutes(30).Seconds;
            command.CommandTimeout = 60000;
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
