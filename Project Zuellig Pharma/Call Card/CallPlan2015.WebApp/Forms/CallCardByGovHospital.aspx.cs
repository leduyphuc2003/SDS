using System;
using System.Collections;
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
    public partial class CallCardByGovHospital : BasePage
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

        //select data tu as400 va bind vao repeater de show ra web
        private void LoadAllPharmacy()
        {
            var originalData = CreateList();
            Session[Constants.SESSION_PHARMACY_CUSTOMER_LIST] = originalData;

            //Tao List<PharmacyCustomerData> luu thong tin sau khi truy van query tu as400
            var data = CreateList();
            // Tao List<PharmacyCustomerData> sau khi da xu li group theo prn,type_sale,item
            var dataBind = CreateDataForBinding(data);

            //bind List<PharmacyCustomerData> vao data repeater roi show ra web
            //class -> repeater
            rPharmacyCustomer.DataSource = dataBind;
            rPharmacyCustomer.DataBind();

            //sum tong ket qua grandtotal
            ltSumAve6LastMonth.Text = data.Sum(item => item.MtdAllSourcesQuantity).ToString("n2");
            ltSumMtdAllSources.Text = data.Sum(item => item.MtdScSourcesQuantity).ToString("n2");
            //ltSumMtdScSources.Text = data.Sum(item => item.MtdScSourcesValue).ToString("n2");
        }

        //load chi tiet khi click vao customer
        private void LoadDetailPharmacy(string prnCode)
        {
            var pharmacyCustomerList = (List<PharmacyCustomerData>)Session[Constants.SESSION_PHARMACY_CUSTOMER_LIST];
            var detailList = pharmacyCustomerList.Where(item => (item.PrnCode == prnCode) && (item.BestSaleFlag == "O")).ToList();

            var otherData = new PharmacyCustomerData()
            {
                PrnCode = "Sum",
                Ave6LastMonthValue = pharmacyCustomerList.Where(item => (item.BestSaleFlag == "O") && (item.PrnCode == prnCode))
                                                         .Sum(item => item.Ave6LastMonthValue),
                MtdAllSourcesValue = pharmacyCustomerList.Where(item => (item.BestSaleFlag == "O") && (item.PrnCode == prnCode))
                                                         .Sum(item => item.MtdAllSourcesValue),
                MtdScSourcesValue = pharmacyCustomerList.Where(item => (item.BestSaleFlag == "O") && (item.PrnCode == prnCode))
                                                        .Sum(item => item.MtdScSourcesValue)
            };

            detailList.Add(otherData);

            rPharmacyCustomer.DataSource = detailList;
            rPharmacyCustomer.DataBind();
        }

        //tao List<PharmacyCustomerData> chua duoc group gi: tao cau query roi truy van va luu vao class PharmacyCustomerData
        private List<PharmacyCustomerData> CreateList()
        {
            //lay cust truyen tu callplan vao callcard
            string custCode = Request[Constants.REQUEST_CUST_CODE];

            //tao cau query
            //dung as400
            //            string query = string.Format(@"SELECT A.CUST, A.CNAME, A.CADD1, A.CMPHON, A.CMCONT, A.ARTRM,      
            //                                            A.SLSMN, A.SMDESC, A.PRNCPL, A.ZCITYP, A.ITEM, A.IMDESC, A.QUOTA,  
            //                                            A.NET_QTY, VALUE(B.IMSOH,0) as IMSOH
            //                                            FROM ZGOVHOSP2 A LEFT JOIN ZCMIMSOH B  
            //                                            ON A.CUST=B.CUST AND A.ITEM=B.ITEM 
            //                                            WHERE a.cust= '{0}' and         
            //                                            a.slsmn= '{1}' ORDER BY A.PRNCPL, A.ZCITYP, A.ITEM                  
            //                                            ", custCode, Session[Constants.SESSION_USER_NAME]);

            //dung SQL thay cho AS400
            string query = string.Format(@"SELECT A.CUST, A.CNAME, A.CADD1, A.CMPHON, A.CMCONT, A.ARTRM,      
                                        A.SLSMN, A.SMDESC, A.PRNCPL, A.ZCITYP, A.ITEM, A.IMDESC, A.QUOTA,  
                                        A.NET_QTY, B.IMSOH as IMSOH,A.REM_QUOTA
                                        FROM ZGOVHOSP2 A LEFT JOIN ZCMIMSOH B  
                                        ON A.CUST=B.CUST AND A.ITEM=B.ITEM 
                                        WHERE a.cust= '{0}' and         
                                        a.slsmn= '{1}' ORDER BY A.PRNCPL, A.ZCITYP, A.ITEM               
                                        ", custCode, Session[Constants.SESSION_USER_NAME]);

            //truy van cau lenh sql bang tham so query o tren roi luu vao mot bang table
            //var table = CallPlanService.GetCustomerData(query);

            // dung SQL thay cho AS400
            var table = CallPlanService.GetCustomerDataUseSQL(query);

            //add ket qua table sau khi truy van tu as400 vao class PharmacyCustomerData(cot du lieu se truyen vao repeater)
            //Tra ve mot list List<PharmacyCustomerData> gom cac row data 
            return CallPlanUtility.ConvertGovHospitalTableToList(table);
        }

        //tao List<PharmacyCustomerData> đã được xử lí cho đúng format với cái sheet excel để bind vào repeater
        private List<PharmacyCustomerData> CreateDataForBinding(List<PharmacyCustomerData> data)
        {
            var originalData = (List<PharmacyCustomerData>)Session[Constants.SESSION_PHARMACY_CUSTOMER_LIST];
            var result = new List<PharmacyCustomerData>();

            //var grouppedData = data.Where(item => item.BestSaleFlag == "B")
            //                       .OrderBy(item => item.PrnCode)
            //                       .GroupBy(item => item.PrnCode)
            //                       .ToDictionary(item => item.Key, item => item.OrderBy(code => code.ProCode).ToList());

            //Group by MTD All sources
            var grouppedData = data.Where(item => item.BestSaleFlag == "B")
                       .OrderBy(item => item.PrnCode)
                       .GroupBy(item => item.PrnCode)
                       .ToDictionary(item => item.Key, item => item.OrderByDescending(code => code.MtdScSourcesQuantity).ToList());

            //vòng lặp cho từng principal: key = principal
            foreach (var key in grouppedData.Keys)
            {
                // clear Prn code except first record
                grouppedData[key].ForEach(item => item.PrnCode = string.Empty);
                //foreach (var item in grouppedData[key])
                //{
                //    item.PrnCode = string.Empty;
                //}
                grouppedData[key][0].PrnCode = key;

                //tong sum cua other
                var otherData = new PharmacyCustomerData()
                {
                    PrnCode = "Others " + key,
                    //ProCode = "Can unhide for all SKUs",
                    ProCode = "",
                    Ave6LastMonthValue = originalData.Where(item => (item.BestSaleFlag == "O") && (item.PrnCode == key))
                                             .Sum(item => item.Ave6LastMonthValue),
                    MtdAllSourcesValue = originalData.Where(item => (item.BestSaleFlag == "O") && (item.PrnCode == key))
                                             .Sum(item => item.MtdAllSourcesValue),
                    MtdScSourcesValue = originalData.Where(item => (item.BestSaleFlag == "O") && (item.PrnCode == key))
                                             .Sum(item => item.MtdScSourcesValue)
                };
                grouppedData[key].Add(otherData);

                //tong sum cua Best va other
                otherData = new PharmacyCustomerData()
                {
                    PrnCode = "Sum by " + key,
                    Ave6LastMonthValue = originalData.Where(item => item.PrnCode == key).Sum(item => item.Ave6LastMonthValue),
                    MtdAllSourcesValue = originalData.Where(item => item.PrnCode == key).Sum(item => item.MtdAllSourcesValue),
                    MtdScSourcesValue = originalData.Where(item => item.PrnCode == key).Sum(item => item.MtdScSourcesValue)
                };
                grouppedData[key].Add(otherData);

                result.AddRange(grouppedData[key]);
            }

            return result;
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
                //var connection = new SQLConnectionUtility();
                //connection.OpenConnection();
                //CallPlanService.InsertToReportUseSQl(cust, ScCode, date, time, connection);
                //connection.CloseConnection();

                CallCardEntities2 cc = new CallCardEntities2();
                ZCPLD tmp = new ZCPLD() {SLSMN = ScCode,CUST = cust,CPLUPD = date,CPLUPT = time};
                cc.ZCPLDs.Add(tmp);
                cc.SaveChanges();

            }
        }
    }
}