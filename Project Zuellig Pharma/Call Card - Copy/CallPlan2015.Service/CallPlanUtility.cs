using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CallPlan2015.Common;
using CallPlan2015.DataModel;

namespace CallPlan2015.Service
{
	public class CallPlanUtility
	{
        //data as400 callplan -> class callPlanData
		public static List<CallPlanData> ConvertCustomerTableToList(DataTable callPlanData)
		{
			var customerList = new List<CallPlanData>();

			foreach (DataRow row in callPlanData.Rows)
			{
				var data = new CallPlanData()
				{
                    //add by ph
                    //add data tung cot select tu as400 vao class CallPLanData
                    //CallPlanDate = (row[Constants.CALL_PLAN_DATE] != DBNull.Value) ? row[Constants.CALL_PLAN_DATE].ToString() : string.Empty,
                    SalespersonCode = (row[Constants.Salesperson_Code] != DBNull.Value) ? row[Constants.Salesperson_Code].ToString() : string.Empty,
                    SalespersonsName = (row[Constants.Salespersons_Name] != DBNull.Value) ? row[Constants.Salespersons_Name].ToString() : string.Empty,
                    PDSalesTeamCode = (row[Constants.PD_Sales_Team_Code] != DBNull.Value) ? row[Constants.PD_Sales_Team_Code].ToString() : string.Empty,
                    //PDSalesTeamName = (row[Constants.PD_Sales_Team_Name] != DBNull.Value) ? row[Constants.PD_Sales_Team_Name].ToString() : string.Empty,
                    LeftWD = (row[Constants.Left_WD] != DBNull.Value) ? Convert.ToInt32(row[Constants.Left_WD]) : 0,

                    //them
                    PassedWD = (row[Constants.PassedWD] != DBNull.Value) ? Convert.ToInt32(row[Constants.PassedWD]) : 0,

                    TargetMTD = (row[Constants.Target_Today] != DBNull.Value) ? Convert.ToDouble(row[Constants.Target_MTD]) : 0,
                    SalesMTD = (row[Constants.Sales_MTD] != DBNull.Value) ?  Convert.ToDouble(row[Constants.Sales_MTD]) : 0,
                    MonthToGo = (row[Constants.Month_To_Go] != DBNull.Value) ?  Convert.ToDouble(row[Constants.Month_To_Go]): 0,
                    TargetToday = (row[Constants.Target_Today] != DBNull.Value) ?  Convert.ToDouble(row[Constants.Target_Today]) :0,
				
					CustomerCode = (row[Constants.CUSTOMER_CUSTOMER_CODE] != DBNull.Value) ? row[Constants.CUSTOMER_CUSTOMER_CODE].ToString() : string.Empty,
					CustomerName = (row[Constants.CUSTOMER_CUSTOMER_NAME] != DBNull.Value) ? row[Constants.CUSTOMER_CUSTOMER_NAME].ToString() : string.Empty,
					Address = (row[Constants.CUSTOMER_ADDRESS] != DBNull.Value) ? row[Constants.CUSTOMER_ADDRESS].ToString() : string.Empty,
					District = (row[Constants.CUSTOMER_DISTRICT] != DBNull.Value) ? row[Constants.CUSTOMER_DISTRICT].ToString() : string.Empty,
					Class = (row[Constants.CUSTOMER_CLASS] != DBNull.Value) ? row[Constants.CUSTOMER_CLASS].ToString() : string.Empty,
					Ave6LastMonth = (row[Constants.CUSTOMER_AVE6_LAST_MONTH] != DBNull.Value) ? Convert.ToDouble(row[Constants.CUSTOMER_AVE6_LAST_MONTH]) : 0.0,
					MtdAllSources = (row[Constants.CUSTOMER_MTD_ALL_SOURCES] != DBNull.Value) ? Convert.ToDouble(row[Constants.CUSTOMER_MTD_ALL_SOURCES]) : 0.0,
					MtdScSources = (row[Constants.CUSTOMER_MTD_SC_SOURCE] != DBNull.Value) ? Convert.ToDouble(row[Constants.CUSTOMER_MTD_SC_SOURCE]) : 0.0,
                    PercentSaleBySc = (row[Constants.CUSTOMER_PERCENT_SALE_BY_SC] != DBNull.Value) ? Convert.ToDouble(row[Constants.CUSTOMER_PERCENT_SALE_BY_SC]) : 0.0,
                    Zlvl = (row[Constants.ZLLVL] != DBNull.Value) ? Convert.ToDouble(row[Constants.ZLLVL]) : 0.0

				};
				// Update percent sale by sc
				//data.PercentSaleBySc = Math.Round(data.MtdScSources/data.MtdAllSources, 2);

                //add nhieu dong vao mot list cac dong
				customerList.Add(data);
			}
            //return ve mot danh sach cac dong data cho bang callplan
			return customerList;
		}

        //add data tu cac cot select tu as400 vao class PharmacyCustomerData(cot du lieu se truyen vao repeater)
        //data as400 -> class PharmacyCustomerData
	    public static List<PharmacyCustomerData> ConvertPharmacyCustomerTableToList(DataTable pharmacyCustomerData)
	    {
	        var pharmacyCustomerList = new List<PharmacyCustomerData>();

            foreach (DataRow row in pharmacyCustomerData.Rows)
	        {
	            var data = new PharmacyCustomerData()   
	            {
                    Cust = (row[Constants.PHARMACY_CUSTOMER_CUST] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_CUST].ToString() : string.Empty,
                    CustName = (row[Constants.PHARMACY_CUSTOMER_CNAME] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_CNAME].ToString() : string.Empty,
                    CustomerAddress = (row[Constants.PHARMACY_CUSTOMER_CADD1] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_CADD1].ToString() : string.Empty,
                    ContactName = (row[Constants.PHARMACY_CUSTOMER_CMCONT] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_CMCONT].ToString() : string.Empty,
                    PhoneNumber = (row[Constants.PHARMACY_CUSTOMER_CMPHON] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_CMPHON].ToString() : string.Empty,
                    TermsCode = (row[Constants.PHARMACY_CUSTOMER_ARTRM] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_ARTRM].ToString() : string.Empty,
                    CustomerLocalGroup1 = (row[Constants.PHARMACY_CUSTOMER_CMGRP2] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_CMGRP2].ToString() : string.Empty,
                    SalespersonCode = (row[Constants.PHARMACY_CUSTOMER_SLSMN] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_SLSMN].ToString() : string.Empty,
                    SMDESC = (row[Constants.PHARMACY_CUSTOMER_SMDESC] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_SMDESC].ToString() : string.Empty,
	                PrnCode = (row[Constants.PHARMACY_CUSTOMER_PRN_CODE] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_PRN_CODE].ToString() : string.Empty,
                    ProCode = (row[Constants.PHARMACY_CUSTOMER_PRO_CODE] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_PRO_CODE].ToString() : string.Empty,
                    Description = (row[Constants.PHARMACY_CUSTOMER_DESCRIPTION] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_DESCRIPTION].ToString() : string.Empty,
                    Ave6LastMonthValue = (row[Constants.PHARMACY_CUSTOMER_AVE6_LAST_MONTH_VALUE] != DBNull.Value) ? Convert.ToDouble(row[Constants.PHARMACY_CUSTOMER_AVE6_LAST_MONTH_VALUE]) : 0.0,
                    MtdAllSourcesValue = (row[Constants.PHARMACY_CUSTOMER_MTD_ALL_SOURCE_VALUE] != DBNull.Value) ? Convert.ToDouble(row[Constants.PHARMACY_CUSTOMER_MTD_ALL_SOURCE_VALUE]) : 0.0,
                    MtdScSourcesValue = (row[Constants.PHARMACY_CUSTOMER_MTD_SC_SOURCE_VALUE] != DBNull.Value) ? Convert.ToDouble(row[Constants.PHARMACY_CUSTOMER_MTD_SC_SOURCE_VALUE]) : 0.0,
                    Ave6LastMonthQuantity = (row[Constants.PHARMACY_CUSTOMER_AVE6_LAST_MONTH_QUANTITY] != DBNull.Value) ? Convert.ToDouble(row[Constants.PHARMACY_CUSTOMER_AVE6_LAST_MONTH_QUANTITY]) : 0.0,
                    MtdAllSourcesQuantity = (row[Constants.PHARMACY_CUSTOMER_MTD_ALL_SOURCE_QUANTITY] != DBNull.Value) ? Convert.ToDouble(row[Constants.PHARMACY_CUSTOMER_MTD_ALL_SOURCE_QUANTITY]) : 0.0,
                    MtdScSourcesQuantity = (row[Constants.PHARMACY_CUSTOMER_MTD_SC_SOURCE_QUANTITY] != DBNull.Value) ? Convert.ToDouble(row[Constants.PHARMACY_CUSTOMER_MTD_SC_SOURCE_QUANTITY]) : 0.0,
                    CheckStock = (row[Constants.PHARMACY_CUSTOMER_CHECK_STOCK] != DBNull.Value) ? Convert.ToInt32(row[Constants.PHARMACY_CUSTOMER_CHECK_STOCK]) : 0,
                    BestSaleFlag = (row[Constants.PHARMACY_CUSTOMER_BEST_SALE_FLAG] != DBNull.Value) ? Convert.ToString(row[Constants.PHARMACY_CUSTOMER_BEST_SALE_FLAG]) : "O"
	            };

                pharmacyCustomerList.Add(data);
	        }
            //return danh sach cac dong data trong bang callcard pharmacy
	        return pharmacyCustomerList;
	    }

	    public static List<PharmacyCustomerData> ConvertPrivateHospitalTableToList(DataTable privateHospitalData)
	    {
            var pharmacyCustomerList = new List<PharmacyCustomerData>();

            foreach (DataRow row in privateHospitalData.Rows)
            {
                var data = new PharmacyCustomerData()
                {
                    Cust = (row[Constants.PHARMACY_CUSTOMER_CUST] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_CUST].ToString() : string.Empty,
                    CustName = (row[Constants.PHARMACY_CUSTOMER_CNAME] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_CNAME].ToString() : string.Empty,
                    CustomerAddress = (row[Constants.PHARMACY_CUSTOMER_CADD1] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_CADD1].ToString() : string.Empty,
                    ContactName = (row[Constants.PHARMACY_CUSTOMER_CMCONT] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_CMCONT].ToString() : string.Empty,
                    PhoneNumber = (row[Constants.PHARMACY_CUSTOMER_CMPHON] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_CMPHON].ToString() : string.Empty,
                    TermsCode = (row[Constants.PHARMACY_CUSTOMER_ARTRM] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_ARTRM].ToString() : string.Empty,
                    CustomerLocalGroup1 = (row[Constants.PHARMACY_CUSTOMER_CMGRP2] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_CMGRP2].ToString() : string.Empty,
                    SalespersonCode = (row[Constants.PHARMACY_CUSTOMER_SLSMN] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_SLSMN].ToString() : string.Empty,
                    SMDESC = (row[Constants.PHARMACY_CUSTOMER_SMDESC] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_SMDESC].ToString() : string.Empty,
                    PrnCode = (row[Constants.PHARMACY_CUSTOMER_PRN_CODE] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_PRN_CODE].ToString() : string.Empty,
                    ProCode = (row[Constants.PHARMACY_CUSTOMER_PRO_CODE] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_PRO_CODE].ToString() : string.Empty,
                    Description = (row[Constants.PHARMACY_CUSTOMER_DESCRIPTION] != DBNull.Value) ? row[Constants.PHARMACY_CUSTOMER_DESCRIPTION].ToString() : string.Empty,

                    //sua 6 dong nay, con lai giong
                    Ave6LastMonthValue = (row[Constants.PRIVATE_HOSPITAL_Avelastmonths] != DBNull.Value) ? Convert.ToDouble(row[Constants.PRIVATE_HOSPITAL_Avelastmonths]) : 0.0,
                    MtdAllSourcesValue = (row[Constants.PRIVATE_HOSPITAL_MTDAllsources] != DBNull.Value) ? Convert.ToDouble(row[Constants.PRIVATE_HOSPITAL_MTDAllsources]) : 0.0,
                    MtdScSourcesValue = (row[Constants.PRIVATE_HOSPITAL_MTDSCsources] != DBNull.Value) ? Convert.ToDouble(row[Constants.PRIVATE_HOSPITAL_MTDSCsources]) : 0.0,
                    Ave6LastMonthQuantity = (row[Constants.PRIVATE_HOSPITAL_Avg6MQty] != DBNull.Value) ? Convert.ToDouble(row[Constants.PRIVATE_HOSPITAL_Avg6MQty]) : 0.0,
                    MtdAllSourcesQuantity = (row[Constants.PRIVATE_HOSPITAL_MTDQtyCI] != DBNull.Value) ? Convert.ToDouble(row[Constants.PRIVATE_HOSPITAL_MTDQtyCI]) : 0.0,
                    MtdScSourcesQuantity = (row[Constants.PRIVATE_HOSPITAL_MTDQtyCIS] != DBNull.Value) ? Convert.ToDouble(row[Constants.PRIVATE_HOSPITAL_MTDQtyCIS]) : 0.0,

                    CheckStock = (row[Constants.PHARMACY_CUSTOMER_CHECK_STOCK] != DBNull.Value) ? Convert.ToInt32(row[Constants.PHARMACY_CUSTOMER_CHECK_STOCK]) : 0,
                    BestSaleFlag = (row[Constants.PHARMACY_CUSTOMER_BEST_SALE_FLAG] != DBNull.Value) ? Convert.ToString(row[Constants.PHARMACY_CUSTOMER_BEST_SALE_FLAG]) : "O"
                };

                pharmacyCustomerList.Add(data);
            }
            //return danh sach cac dong data trong bang callcard pharmacy
            return pharmacyCustomerList;
	    }

        public static List<PharmacyCustomerData> ConvertGovHospitalTableToList(DataTable pharmacyCustomerData)
        {
            var pharmacyCustomerList = new List<PharmacyCustomerData>();

            foreach (DataRow row in pharmacyCustomerData.Rows)
            {
                var data = new PharmacyCustomerData()
                {


                    Cust = (row[Constants.GOV_HOSPITAL_CUST] != DBNull.Value) ? row[Constants.GOV_HOSPITAL_CUST].ToString() : string.Empty,
                    CustName = (row[Constants.GOV_HOSPITAL_CNAME] != DBNull.Value) ? row[Constants.GOV_HOSPITAL_CNAME].ToString() : string.Empty,
                    CustomerAddress = (row[Constants.GOV_HOSPITAL_CADD1] != DBNull.Value) ? row[Constants.GOV_HOSPITAL_CADD1].ToString() : string.Empty,
                    PhoneNumber = (row[Constants.GOV_HOSPITAL_CMPHON] != DBNull.Value) ? row[Constants.GOV_HOSPITAL_CMPHON].ToString() : string.Empty,
                    ContactName = (row[Constants.GOV_HOSPITAL_CMCONT] != DBNull.Value) ? row[Constants.GOV_HOSPITAL_CMCONT].ToString() : string.Empty,
                    TermsCode = (row[Constants.GOV_HOSPITAL_ARTRM] != DBNull.Value) ? row[Constants.GOV_HOSPITAL_ARTRM].ToString() : string.Empty,
                    SalespersonCode = (row[Constants.GOV_HOSPITAL_SLSMN] != DBNull.Value) ? row[Constants.GOV_HOSPITAL_SLSMN].ToString() : string.Empty,
                    PrnCode = (row[Constants.GOV_HOSPITAL_PRNCPL] != DBNull.Value) ? row[Constants.GOV_HOSPITAL_PRNCPL].ToString() : string.Empty,
                    BestSaleFlag = (row[Constants.GOV_HOSPITAL_ZCITYP] != DBNull.Value) ? Convert.ToString(row[Constants.GOV_HOSPITAL_ZCITYP]) : "O",
                    ProCode = (row[Constants.GOV_HOSPITAL_ITEM] != DBNull.Value) ? row[Constants.GOV_HOSPITAL_ITEM].ToString() : string.Empty,
                    Description = (row[Constants.GOV_HOSPITAL_IMSDES] != DBNull.Value) ? row[Constants.GOV_HOSPITAL_IMSDES].ToString() : string.Empty,
                    SMDESC = (row[Constants.GOV_HOSPITAL_SC_NAME] != DBNull.Value) ? row[Constants.GOV_HOSPITAL_SC_NAME].ToString() : string.Empty,
                    
                    //quota va net qty
                    MtdAllSourcesQuantity = (row[Constants.GOV_HOSPITAL_QUOTA] != DBNull.Value) ? Convert.ToDouble(row[Constants.GOV_HOSPITAL_QUOTA]) : 0.0,
                    MtdScSourcesQuantity = (row[Constants.GOV_HOSPITAL_NET_QTY] != DBNull.Value) ? Convert.ToDouble(row[Constants.GOV_HOSPITAL_NET_QTY]) : 0.0,
                    CheckStock = (row[Constants.PHARMACY_CUSTOMER_CHECK_STOCK] != DBNull.Value) ? Convert.ToInt32(row[Constants.PHARMACY_CUSTOMER_CHECK_STOCK]) : 0,
                    
                };

                pharmacyCustomerList.Add(data);
            }
            //return danh sach cac dong data trong bang callcard pharmacy
            return pharmacyCustomerList;
        }

	}
}
