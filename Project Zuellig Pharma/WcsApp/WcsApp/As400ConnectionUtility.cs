using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2.iSeries;

namespace WcsApp
{
 
        public class As400ConnectionUtility
        {
            private iDB2Connection m_connection = null;

            public As400ConnectionUtility()
            {
                //string conec = "DataSource=192.168.17.2;userid=VIEOPR;password=vnzp456;DefaultCollection=ZPTLIBD;Pooling=true;Connection Timeout=0;CheckConnectionOnOpen=true;EnablePreFetch=false";
                //m_connection = new iDB2Connection(conec);

                m_connection = new iDB2Connection();
                m_connection.ConnectionString = "DataSource=192.168.17.2;userid=VIEOPR;password=vnzp456;DefaultCollection=ZPTLIBD;Pooling=true;Connection Timeout=0;CheckConnectionOnOpen=true;EnablePreFetch=false";
                m_connection.Open();
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
            /// select data
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

            //insert updpate data
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
                catch (Exception ex)
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
