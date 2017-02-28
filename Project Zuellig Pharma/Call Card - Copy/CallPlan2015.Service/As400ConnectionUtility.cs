using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using IBM.Data.DB2.iSeries;

namespace CallPlan2015.Service
{
    public class As400ConnectionUtility
    {
        private iDB2Connection m_connection = null;

        public As400ConnectionUtility()
        {
            m_connection = new iDB2Connection(ConfigurationManager.ConnectionStrings["CallPlanConnectionString"].ConnectionString);
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

        /// <summary>
        /// Get data by query
        /// </summary>
        /// <param name="queryString">The query string: Example Select * from table where condition</param>
        /// <returns>Data table</returns>
        public DataTable GetDataByQuery(string queryString, Hashtable parameters = null)
        {
            // open connection
            OpenConnection();

            // Get data
            var result = new DataTable();
            var command = new iDB2Command(queryString, m_connection);
            
            //add by PH
            command.CommandTimeout = 0;

            if (parameters != null)
            {
                foreach (var paramName in parameters.Keys)
                {
                    command.Parameters.Add(parameters.ToString(), parameters[paramName]);
                }
            }

            var adapter = new iDB2DataAdapter(command);

            try
            {
                adapter.Fill(result);
            }
            catch
            {
                result = null;
            }
            finally
            {
                // Close connection
                CloseConnection();
            }

            return result;
        }

        public string ExecuteQuery(string queryString, Hashtable parameters = null)
        {
            var error = new StringBuilder();

            // open connection
            OpenConnection();

            var command = new iDB2Command(queryString, m_connection);

            //add by PH
            command.CommandTimeout = 0;

            if (parameters != null)
            {
                foreach (var paramName in parameters.Keys)
                {
                    command.Parameters.Add(parameters.ToString(), parameters[paramName]);
                }
            }

            try
            {
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                error.Append(ex.Message).Append(Environment.NewLine);
            }
            finally
            {
                // Close connection
                CloseConnection();
            }

            return error.ToString();
        }
    }
}
