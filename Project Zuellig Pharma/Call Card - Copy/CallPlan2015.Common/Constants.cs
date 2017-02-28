using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CallPlan2015.Common
{
	public class Constants
	{
        // Columns name of Table callplan by cus
        //add by ph: dinh nghia ten cot trong as400
        public const string CALL_PLAN_DATE = "CPLDTE";
        public const string Salesperson_Code = "SLSMN";
        public const string Salespersons_Name = "SMDESC";
        public const string PD_Sales_Team_Code = "PDTCDE";
        public const string PD_Sales_Team_Name = "PDTDES";
        public const string Left_WD = "$JNMAH";
        public const string PassedWD = "$JNMAR";
        public const string Target_MTD = "$JNMAD";
        public const string Sales_MTD = "$JNMAQ";
        public const string Month_To_Go = "$JNMAF";
        public const string Target_Today = "$JNMAI";
        public const string CUSTOMER_CUSTOMER_CODE = "CUST";
        public const string CUSTOMER_CUSTOMER_NAME = "CNAME";
        public const string Customer_Local_Group1 = "CMGRP2";
        public const string CUSTOMER_ADDRESS = "CADD1";
        public const string CUSTOMER_DISTRICT = "CADD2";
        public const string CUSTOMER_CLASS = "ZCMTYP";
        public const string CUSTOMER_AVE6_LAST_MONTH = "$JNMAE";
        public const string CUSTOMER_MTD_ALL_SOURCES = "$JNMAO";
        public const string CUSTOMER_MTD_SC_SOURCE = "$JNMAP";
        public const string CUSTOMER_PERCENT_SALE_BY_SC = "$JNMAG";
        public const string ZLLVL = "ZLVL";

        //add by ph for callcard pharmacy + add for callcard GL pharmacy
        public const string PHARMACY_CUSTOMER_PRN_CODE = "PRNCPL";
        public const string PHARMACY_CUSTOMER_PRO_CODE = "ITEM";
        public const string PHARMACY_CUSTOMER_DESCRIPTION = "IMDESC";
        public const string PHARMACY_CUSTOMER_AVE6_LAST_MONTH_VALUE = "$JNNAD";
        public const string PHARMACY_CUSTOMER_MTD_ALL_SOURCE_VALUE = "$JNNAE";
        public const string PHARMACY_CUSTOMER_MTD_SC_SOURCE_VALUE = "$JNNAF";
        public const string PHARMACY_CUSTOMER_AVE6_LAST_MONTH_QUANTITY = "$JNNAA";
        public const string PHARMACY_CUSTOMER_MTD_ALL_SOURCE_QUANTITY = "$JNNAB";
        public const string PHARMACY_CUSTOMER_MTD_SC_SOURCE_QUANTITY = "$JNNAC";
        public const string PHARMACY_CUSTOMER_CHECK_STOCK = "IMSOH";
        public const string PHARMACY_CUSTOMER_BEST_SALE_FLAG = "ZCITYP";

        public const string PHARMACY_CUSTOMER_CUST = "CUST";
        public const string PHARMACY_CUSTOMER_CNAME = "CNAME";
        public const string PHARMACY_CUSTOMER_CADD1 = "CADD1";
        public const string PHARMACY_CUSTOMER_CMCONT = "CMCONT";
        public const string PHARMACY_CUSTOMER_CMPHON = "CMPHON";
        public const string PHARMACY_CUSTOMER_ARTRM = "ARTRM";
        public const string PHARMACY_CUSTOMER_CMGRP2 = "CMGRP2";
        public const string PHARMACY_CUSTOMER_PDTCDE = "PDTCDE";
        public const string PHARMACY_CUSTOMER_SLSMN = "SLSMN";
        public const string PHARMACY_CUSTOMER_SMDESC = "SMDESC";
        public const string PHARMACY_CUSTOMER_ZCITYP = "ZCITYP";
        public const string PHARMACY_CUSTOMER_PRNCPL = "PRNCPL";
        public const string PHARMACY_CUSTOMER_ITEM = "ITEM";
        public const string PHARMACY_CUSTOMER_IMDESC = "IMDESC";
        public const string PHARMACY_CUSTOMER_JNNAD = "$JNNAD";
        public const string PHARMACY_CUSTOMER_JNNAE = "$JNNAE";
        public const string PHARMACY_CUSTOMER_JNNAF = "$JNNAF";
        public const string PHARMACY_CUSTOMER_JNNAA = "$JNNAA";
        public const string PHARMACY_CUSTOMER_JNNAB = "$JNNAB";
        public const string PHARMACY_CUSTOMER_JNNAC = "$JNNAC";
        
        //add for callcard private hospital
        public const string PRIVATE_HOSPITAL_CUST = "CUST";
        public const string PRIVATE_HOSPITAL_CUSTNAME = "CNAME";
        public const string PRIVATE_HOSPITAL_ADDRESS = "CADD1";
        public const string PRIVATE_HOSPITAL_Contact_Name = "CMCONT";
        public const string PRIVATE_HOSPITAL_Phone_Number = "CMPHON";
        public const string PRIVATE_HOSPITAL_CustomerLocalGroup = "CMGRP2";
        public const string PRIVATE_HOSPITAL_PDSalesTeamCode = "PDTCDE";
        public const string PRIVATE_HOSPITAL_SalespersonCode = "SLSMN";
        public const string PRIVATE_HOSPITAL_SalespersonsName = "SMDESC";
        public const string PRIVATE_HOSPITAL_CallCardItemType = "ZCITYP";
        public const string PRIVATE_HOSPITAL_PrincipalCode = "PRNCPL";
        public const string PRIVATE_HOSPITAL_ItemCode = "ITEM";
        public const string PRIVATE_HOSPITAL_ItemDescription = "IMDESC";
        public const string PRIVATE_HOSPITAL_Avelastmonths = "$JNQAD";
        public const string PRIVATE_HOSPITAL_MTDAllsources = "$JNQAE";
        public const string PRIVATE_HOSPITAL_MTDSCsources = "$JNQAF";
        public const string PRIVATE_HOSPITAL_Avg6MQty = "$JNQAA";
        public const string PRIVATE_HOSPITAL_MTDQtyCI = "$JNQAB";
        public const string PRIVATE_HOSPITAL_MTDQtyCIS = "$JNQAC";


        //add for callcard Gov Hospital
        public const string GOV_HOSPITAL_CUST = "CUST";
        public const string GOV_HOSPITAL_CNAME = "CNAME";
        public const string GOV_HOSPITAL_CADD1 = "CADD1";
        public const string GOV_HOSPITAL_CMPHON = "CMPHON";
        public const string GOV_HOSPITAL_CMCONT = "CMCONT";
        public const string GOV_HOSPITAL_ARTRM = "ARTRM";
        public const string GOV_HOSPITAL_SLSMN = "SLSMN";
        public const string GOV_HOSPITAL_PRNCPL = "PRNCPL";
        public const string GOV_HOSPITAL_ZCITYP = "ZCITYP";
        public const string GOV_HOSPITAL_ITEM = "ITEM";
        public const string GOV_HOSPITAL_IMSDES = "IMDESC";
        public const string GOV_HOSPITAL_QUOTA = "QUOTA";
        public const string GOV_HOSPITAL_NET_QTY = "NET_QTY";
        //them
        public const string GOV_HOSPITAL_SC_NAME = "SMDESC";

	    public const string SESSION_PHARMACY_CUSTOMER_LIST = "session_pharmacy_customer_list";
	    public const string SESSION_USER_NAME = "session_user_name";
	    public const string SESSION_ERROR = "session_error";
	    public const string SESSION_ACTIVE_PAGE = "session_active_page";
	    public const string REQUEST_CUST_CODE = "cc";

        public const string SESSION_PHARMACY_DATE = "session_pharmacy_date";
        public const string SESSION_PHARMACY_DATE_SHOW = "session_pharmacy_date_show";

        public const string SESSION_GL_Pharmacy_DATE = "session_gl_pharmacy_date";
        public const string SESSION_GL_Pharmacy_DATE_SHOW = "session_gl_pharmacy_date_show";

        public const string SESSION_GOV_HOS_DATE = "session_gv_hos_date";
        public const string SESSION_GOV_HOS_DATE_SHOW = "session_gv_hos_date_show";

        public const string SESSION_PRI_HOS_DATE = "session_pri_hos_date";
        public const string SESSION_PRI_HOS_DATE_SHOW = "session_pri_hos_date_show";

        public const string SESSION_EXCLUSIVE_DATE = "session_exclusive_date";
        public const string SESSION_EXCLUSIVE_DATE_SHOW = "session_exclusive_date_show";

        public const string SESSION_DAYS_GONE_BY = "session_days_gone_by";
        public const string SESSION_Days_Left_In_Month  = "session_days_left_in_month";
        public const string SESSION_Total_Days_In_Month = "session_total_days_in_month";
        public const string SESSION_OF_Month_Gone_By = "session_of_month_gone_by";
	}
}
