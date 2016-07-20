using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using CallPlan2015.Common;
using CallPlan2015.DataModel;
using CallPlan2015.Service;
using CallPlan2015.WebApp.Common;

namespace CallPlan2015.WebApp.Forms
{
    public partial class CallCardByGLPharmacy : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.SESSION_USER_NAME] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                //neu tham so truyen qua cc co gia tri thi load data
                if (!string.IsNullOrEmpty(Request[Constants.REQUEST_CUST_CODE]))
                {
                    var originalData = CreateList();
                    Session[Constants.SESSION_PHARMACY_CUSTOMER_LIST] = originalData;

                    LoadAllPharmacy();
                    InsertReport();
                }
                else if (string.IsNullOrEmpty(Request["detail"]) == false)
                {
                    LoadDetailPharmacy(Request["detail"]);
                }

                InitMasterData();
            }
        }

        private void LoadAllPharmacy()
        {
            var originalData = (List<PharmacyCustomerData>)Session[Constants.SESSION_PHARMACY_CUSTOMER_LIST];
            var data = CreateList();

            // them .OrderByDescending(item => item.Ave6LastMonthValue)
            data = data.Where(item => item.BestSaleFlag == "B").OrderByDescending(item => item.Ave6LastMonthValue).ToList();

            // add them others
            data.Add(new PharmacyCustomerData()
            {
                PrnCode = "Others",
                //ProCode = "Can unhide for all SKUs",
                ProCode = "",
                Ave6LastMonthValue = originalData.Where(item => (item.BestSaleFlag == "O"))
                                             .Sum(item => item.Ave6LastMonthValue),
                MtdAllSourcesValue = originalData.Where(item => (item.BestSaleFlag == "O"))
                                         .Sum(item => item.MtdAllSourcesValue),
                MtdScSourcesValue = originalData.Where(item => (item.BestSaleFlag == "O"))
                                         .Sum(item => item.MtdScSourcesValue)
            });

            rPharmacyCustomer.DataSource = data;
            rPharmacyCustomer.DataBind();

            ltSumAve6LastMonth.Text = data.Sum(item => item.Ave6LastMonthValue).ToString("n2");
            ltSumMtdAllSources.Text = data.Sum(item => item.MtdAllSourcesValue).ToString("n2");
            ltSumMtdScSources.Text = data.Sum(item => item.MtdScSourcesValue).ToString("n2");
        }

        private void LoadDetailPharmacy(string prnCode)
        {
            var pharmacyCustomerList = (List<PharmacyCustomerData>)Session[Constants.SESSION_PHARMACY_CUSTOMER_LIST];
            // them .OrderByDescending(item => item.Ave6LastMonthValue)
            var detailList = pharmacyCustomerList.Where(item => (item.BestSaleFlag == "O")).OrderByDescending(item => item.Ave6LastMonthValue).ToList();

            var otherData = new PharmacyCustomerData()
            {
                PrnCode = "Sum",
                Ave6LastMonthValue = pharmacyCustomerList.Where(item => (item.BestSaleFlag == "O"))
                                                         .Sum(item => item.Ave6LastMonthValue),
                MtdAllSourcesValue = pharmacyCustomerList.Where(item => (item.BestSaleFlag == "O"))
                                                         .Sum(item => item.MtdAllSourcesValue),
                MtdScSourcesValue = pharmacyCustomerList.Where(item => (item.BestSaleFlag == "O"))
                                                        .Sum(item => item.MtdScSourcesValue)
            };

            detailList.Add(otherData);

            rPharmacyCustomer.DataSource = detailList;
            rPharmacyCustomer.DataBind();
        }

        private List<PharmacyCustomerData> CreateList()
        {

            string custCode = Request[Constants.REQUEST_CUST_CODE];
//            string query = string.Format(@"SELECT A.CUST, A.CNAME, A.CADD1, A.CMCONT, A.CMPHON, A.ARTRM,     
//                                            A.CMGRP2, A.PDTCDE, A.SLSMN, A.SMDESC, A.ZCITYP, A.PRNCPL, A.ITEM,
//                                            A.IMDESC, A.$JNNAD, A.$JNNAE, A.$JNNAF, A.$JNNAA, A.$JNNAB,       
//                                            A.$JNNAC ,value(imsoh,0) as IMSOH 
//                                            FROM zsa757pf a left join zcmimsoh b on  
//                                            a.cust=b.cust and a.item=b.item 
//                                            WHERE a.cust= '{0}' and         
//                                            a.slsmn= '{1}' order BY A.ZCITYP, A.ITEM                 
//                                            ", custCode, Session[Constants.SESSION_USER_NAME]);

            //dung SQL thay cho AS400
            string query = string.Format(@"SELECT A.CUST, A.CNAME, A.CADD1, A.CMCONT, A.CMPHON, A.ARTRM,     
                                        A.CMGRP2, A.PDTCDE, A.SLSMN, A.SMDESC, A.ZCITYP, A.PRNCPL, A.ITEM,
                                        A.IMDESC, A.[$JNNAD], A.[$JNNAE], A.[$JNNAF], A.[$JNNAA], A.[$JNNAB],A.[$JNNAC] ,imsoh as IMSOH 
                                        FROM zsa757pf a left join zcmimsoh b on  
                                        a.cust=b.cust and a.item=b.item 
                                        WHERE a.cust= '{0}' and         
                                        a.slsmn= '{1}' order BY A.ZCITYP,A.ITEM                 
                                        ", custCode, Session[Constants.SESSION_USER_NAME]);



            //truy van cau lenh sql bang tham so query o tren roi luu vao mot bang table
            //var table = CallPlanService.GetCustomerData(query);

            // dung SQL thay cho AS400
            var table = CallPlanService.GetCustomerDataUseSQL(query);

            return CallPlanUtility.ConvertPharmacyCustomerTableToList(table);
        }

        //submit stock
        protected void btnSubmitStock_OnClick(object sender, EventArgs e)
        {
            var pharmacyCustomerList = (List<PharmacyCustomerData>)Session[Constants.SESSION_PHARMACY_CUSTOMER_LIST];

            foreach (RepeaterItem item in rPharmacyCustomer.Items)
            {
                var pharmacyCustomer = pharmacyCustomerList.FirstOrDefault(x => x.ProCode == ((Label)item.FindControl("lblProCode")).Text);
                if (pharmacyCustomer != null)
                {
                    pharmacyCustomer.CheckStock = Convert.ToInt32(((TextBox)item.FindControl("txtStock")).Text);
                }
            }
            // Save stock by pro code

            // Save stock by pro code
            //var connection = new As400ConnectionUtility();
            //connection.OpenConnection();
            //foreach (var pharmacyCust in pharmacyCustomerList)
            //{
            //    CallPlanService.InsertUpdateStock(pharmacyCust.Cust, pharmacyCust.ProCode, pharmacyCust.CheckStock, connection);
            //}


            //dung sql thay cho as400
            var connection = new SQLConnectionUtility();
            connection.OpenConnection();
            foreach (var pharmacyCust in pharmacyCustomerList)
            {
                CallPlanService.InsertUpdateStockUseSQL(pharmacyCust.Cust, pharmacyCust.ProCode, pharmacyCust.CheckStock, connection);
            }
            connection.CloseConnection();
        }

        protected string CreateInput(PharmacyCustomerData data)
        {
            if (data.PrnCode.Contains("Others") || data.PrnCode.Contains("Sum"))
            {
                return string.Empty;
            }

            return "<asp:TextBox runat='server' ID='txtStock' CssClass='callplan-text' Text='<%# Eval(Constants.PHARMACY_CUSTOMER_CHECK_STOCK) %>'/>";
        }

        private void InitMasterData()
        {
            var sData = new PharmacyCustomerData();
            var originalData = (List<PharmacyCustomerData>)Session[Constants.SESSION_PHARMACY_CUSTOMER_LIST];
            if (originalData.Count > 0)
            {
                sData = originalData[0];
            }

            lblCustCode.InnerText = sData.Cust;
            lblCustName.InnerText = sData.CustName;
            lblCustAddress.InnerText = sData.CustomerAddress;
            lblTelephone.InnerText = sData.PhoneNumber;
            lblContactPerson.InnerText = sData.ContactName;
            lblTerm.InnerText = sData.TermsCode;
            lblScCode.InnerText = sData.SalespersonCode;
            lblScName.InnerText = sData.SMDESC;
        }

        private void InsertReport()
        {
            //var sData = new PharmacyCustomerData();
            //var originalData = (List<PharmacyCustomerData>)Session[Constants.SESSION_PHARMACY_CUSTOMER_LIST];
            //if (originalData.Count > 0)
            //{
            //    sData = originalData[0];
            //}

            //string cust = sData.Cust;
            //string ScCode = sData.SalespersonCode;
            //int date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
            //int time = Int32.Parse(DateTime.Now.ToString("hhmmss"));

            //var connection = new As400ConnectionUtility();
            //connection.OpenConnection();
            //CallPlanService.InsertToReport(cust, ScCode, date, time, connection);
            //connection.CloseConnection();

            string cust = Request[Constants.REQUEST_CUST_CODE];
            string ScCode = Session[Constants.SESSION_USER_NAME].ToString();
            int date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int time = Int32.Parse(DateTime.Now.ToString("HHmmss"));

            //dung as400
            //if ((cust != null) && (ScCode != null) && (date != null) && (time != null))
            //{
            //    var connection = new As400ConnectionUtility();
            //    connection.OpenConnection();
            //    CallPlanService.InsertToReport(cust, ScCode, date, time, connection);
            //    connection.CloseConnection();
            //}


            //dung sql thay cho as400
            if ((cust != null) && (ScCode != null) && (date != null) && (time != null))
            {
                var connection = new SQLConnectionUtility();
                connection.OpenConnection();
                CallPlanService.InsertToReportUseSQl(cust, ScCode, date, time, connection);
                connection.CloseConnection();
            }
        }
    }
}