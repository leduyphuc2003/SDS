using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CallPlan2015.Common;
using CallPlan2015.DataModel;
using CallPlan2015.Service;
using CallPlan2015.WebApp.Common;
 
namespace CallPlan2015.WebApp.Forms
{
	public partial class CallPlanByCus : BasePage
	{protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                //tao bien data luu data truy van sql tu as400
				var data = CreateList();
                //lay data truy van tu as400 do~ vao repeater rCustomer
				rCustomer.DataSource = data;
				rCustomer.DataBind();

                //sum ket qua cac cot data trong repeater
			    ltSumAve6LastMonth.Text = data.Sum(item => item.Ave6LastMonth).ToString("n0");

			    if (data.Count == 0)
			    {
			        Session[Constants.SESSION_ERROR] = "No data!";
			        Session[Constants.SESSION_ACTIVE_PAGE] = Request.Url.AbsoluteUri;
                    Response.Redirect(RedirectUrl("~/Forms/Error.aspx"));
			    }

                // Init master data
			    InitMasterData(data[0]);
			}
		}

        private void InitMasterData(CallPlanData scData)
        {
            lblScCode.InnerText = scData.SalespersonCode;
            lblScName.InnerText = scData.SalespersonsName;
            lblBU.InnerText = scData.PDSalesTeamCode;
            lblLeftWD.InnerText = scData.LeftWD.ToString("n0");
            lblTargetMTD.InnerText = scData.TargetMTD.ToString("n0");
            lblSalesMTD.InnerText = scData.SalesMTD.ToString("n0"); 
            lblMonthToGo.InnerText = scData.MonthToGo.ToString("n0");
            lblTargetToday.InnerText = scData.TargetToday.ToString("n0");


            //++them
            var lblBySCValue = ((scData.SalesMTD/scData.TargetMTD)*100);
            lblBySC.InnerText = lblBySCValue.ToString("n0")+"%";

            lblDaysGoneBy.InnerText = scData.PassedWD.ToString("n0");
            lblTotalDaysInMonth.InnerText = (scData.LeftWD + scData.PassedWD).ToString("n0");
              
            float a = scData.PassedWD;
            float b = scData.LeftWD + scData.PassedWD;
            float c = (a/b)*100;
            lblOfMonthGoneBy.InnerText = c.ToString("n0")+"%";

            // change color of lblBySC
            lblBySC.Attributes.Add("class", (lblBySCValue >= c) ? "foreground-green" : "foreground-red");
            Session[Constants.SESSION_DAYS_GONE_BY] = lblDaysGoneBy.InnerText;
            Session[Constants.SESSION_Days_Left_In_Month] = lblLeftWD.InnerText;
            Session[Constants.SESSION_Total_Days_In_Month] = lblTotalDaysInMonth.InnerText;
            Session[Constants.SESSION_OF_Month_Gone_By] = (int)Math.Ceiling(c);
            //--them
        }

        //truy van sql tu as400
		private List<CallPlanData> CreateList()
		{

		    if (Session[Constants.SESSION_USER_NAME] == null)
		    {
		        Response.Redirect(RedirectUrl("~/Login.aspx"));
		    }

          
            string scCode = Session[Constants.SESSION_USER_NAME].ToString();
            //them ,$JNMAR
//            string query = string.Format(@"SELECT A.SLSMN,A.SMDESC, A.PDTCDE,A.$JNMAH,A.$JNMAR, A.$JNMAD,A.$JNMAQ, A.$JNMAF,A.$JNMAI, A.CUST, 
//                                           A.CNAME, A.CMGRP2, A.CADD1, A.CADD2,A.ZCMTYP, A.$JNMAE,A.$JNMAO,A.$JNMAP, 
//                                           A.$JNMAG, value(B.ZLVL2,0) as ZLVL 
//											FROM zsa756pf a left join zlvl b on a.cmgrp2=b.cmgrp2 and           
//											a.cmgrp3=b.cmgrp3 and a.cmgrp4=b.cmgrp4 and a.zcmtyp=b.zcmtyp WHERE 
//											CPLDTE = {0} and a.slsmn='{1}' 
//                                            AND A.CMGRP2 in ('TD1','PH1') AND PDTCDE NOT in ('GSK','AZE')		
//										", Session[Constants.SESSION_PHARMACY_DATE].ToString(), scCode);

            //dung SQL thay AS400
            string query = string.Format(@"SELECT A.SLSMN,A.SMDESC, A.PDTCDE,[$JNMAH],[$JNMAR], [$JNMAD],[$JNMAQ], [$JNMAF],[$JNMAI],A.CUST, 
                                            A.CNAME, A.CMGRP2, A.CADD1, A.CADD2,A.ZCMTYP, [$JNMAE],[$JNMAO],[$JNMAP], 
                                            [$JNMAG],B.ZLVL2 ZLVL
											FROM zsa756pf a left join zlvl b on a.cmgrp2=b.cmgrp2 and           
											a.cmgrp3=b.cmgrp3 and a.cmgrp4=b.cmgrp4 and a.zcmtyp=b.zcmtyp WHERE 
											CPLDTE = {0} and a.slsmn='{1}' 
                                            AND A.CMGRP2 in ('TD1','PH1') AND PDTCDE NOT in ('GSK','AZE')		
										", Session[Constants.SESSION_PHARMACY_DATE].ToString(), scCode);


            //truy van cau lenh sql bang tham so query o tren roi luu vao mot bang table
            //var table = CallPlanService.GetCustomerData(query);

            // dung SQL
		    var table = CallPlanService.GetCustomerDataUseSQL(query);

            
            //convert table data select tu as400 bang cau lenh o tren vao mot list danh sach cac dong data co the do~ vao repeater
			return CallPlanUtility.ConvertCustomerTableToList(table);
		}
	}
}