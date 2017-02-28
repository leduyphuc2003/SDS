using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;

namespace CallPlan2015.Service
{
    public class CallPlanService
    {
        public static DataTable GetCustomerData(string query)
        {
            var connect = new As400ConnectionUtility();
            return connect.GetDataByQuery(query);
        }

        //dung SQL thay cho as400
        public static DataTable GetCustomerDataUseSQL(string query)
        {
            var connect = new SQLConnectionUtility();
            return connect.GetData(query);
        }

        public static string InsertUpdateStock(string custCode, string item, int stock, As400ConnectionUtility connect)
        {
            string statement = string.Empty;
            string insertStock = string.Format("insert into zcmimsoh(CUST, ITEM, IMSOH) values ('{0}', '{1}', '{2}')", custCode, item, stock);
            string updateStock = string.Format("update zcmimsoh set IMSOH = '{0}' where CUST = '{1}' and ITEM = '{2}'", stock, custCode, item);

            // Check stock
            string checkQuery = string.Format("select * from zcmimsoh where CUST = '{0}' and ITEM = '{1}'", custCode, item);
            
            var data = connect.GetDataByQuery(checkQuery);
            statement = (data.Rows.Count == 0) ? insertStock : updateStock;

            return connect.ExecuteQuery(statement);
        }

        //dung SQL thay cho as400
        public static string InsertUpdateStockUseSQL(string custCode, string item, int stock, SQLConnectionUtility connect)
        {
            //string statement = string.Empty;
            string insertStock = string.Format("insert into zcmimsoh(CUST, ITEM, IMSOH) values ('{0}', '{1}', '{2}')", custCode, item, stock);
            //string updateStock = string.Format("update zcmimsoh set IMSOH = '{0}' where CUST = '{1}' and ITEM = '{2}'", stock, custCode, item);
            string deleteStock = string.Format("delete zcmimsoh where CUST = '{0}' and ITEM = '{1}'", custCode, item);

            // Check stock
            //string checkQuery = string.Format("select * from zcmimsoh where CUST = '{0}' and ITEM = '{1}'", custCode, item);
            //var data = connect.GetData(checkQuery);
            //statement = (data.Rows.Count == 0) ? insertStock : updateStock;

            connect.UpdateData(deleteStock);
            connect.UpdateData(insertStock);
            return "";
        }

        public static string InsertToReport(string cust, string SC, int date, int time, As400ConnectionUtility connect)
        {
            string statement = string.Format("insert into ZCPLD(SLSMN, CUST, CPLUPD,CPLUPT) values ('{0}', '{1}', '{2}','{3}')", SC, cust, date, time);
            try
            {
                return connect.ExecuteQuery(statement);
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        //dung SQL thay cho AS400
        public static string InsertToReportUseSQl(string cust, string SC, int date, int time, SQLConnectionUtility connect)
        {
            string statement = string.Format("insert into ZCPLD(SLSMN, CUST, CPLUPD,CPLUPT) values ('{0}', '{1}', '{2}','{3}')", SC, cust, date, time);
            try
            {
                connect.UpdateData(statement);
                return "";
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
    }
}
