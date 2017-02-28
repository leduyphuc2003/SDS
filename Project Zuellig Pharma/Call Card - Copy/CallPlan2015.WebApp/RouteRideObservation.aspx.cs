using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallPlan2015.Common;
using CallPlan2015.DataModel;
using CallPlan2015.Service;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;

namespace CallPlan2015.WebApp
{
    public partial class RouteRideObservation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.SESSION_USER_NAME] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

                //if (Session["GridData"] == null)
                    Session["GridData"] = CreateData();

                ASPxGridView1.DataSource = Session["GridData"];

                if (!IsPostBack && !IsCallback)
                    ASPxGridView1.DataBind();

                ASPxGridView1.Settings.HorizontalScrollBarMode = ScrollBarMode.Auto;
                ASPxGridView1.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;

        }


        private DataTable CreateData()
        {
            string user = Session[Constants.SESSION_USER_NAME].ToString();
            var sqlHelper = new SQLConnectionUtility();
            string query;
            if (user == "adminob")
            {
                query = "SELECT * FROM RouteRideObservation";
            }
            else
            {
                query = string.Format("SELECT * FROM RouteRideObservation where SCcode='{0}'",user);
            }
            var result = sqlHelper.GetData(query);
            result.PrimaryKey = new DataColumn[]{result.Columns["Id"]};
            user = "";
            return result;
        }


        private bool UpdateData(DataRow row)
        {
            var sqlHelper = new SQLConnectionUtility();
            string updateQuery = string.Format("Update RouteRideObservation set DateCallplan=N'{1}',SC=N'{2}',CustomerCode=N'{3}'," +
                                               "CustomerName=N'{4}',Address=N'{5}',Dist=N'{6}',Area=N'{7}',CustomerClassification=N'{8}'," +
                                               "MonthlyCallFrequency=N'{9}',CustomerType=N'{10}',FFM=N'{11}',ObserverName=N'{12}'," +
                                               "TransferTime=N'{13}',TimeEntering=N'{14}'," +
                                               "MeetCustomer=N'{15}',MeetPharmacyOwner=N'{16}'," +
                                               "MeetPharmacyOrderingStaff=N'{17}',MeetPharmacyAssisstant=N'{18}'," +
                                               "TimeToGreet=N'{19}',TimeToCheckStock=N'{20}'," +
                                               "TimeToTakeOrder=N'{21}',TimeToNegotiate=N'{22}'," +
                                               "TimeToDoSurvey=N'{23}',TimeToDiscussOtherServices=N'{24}'," +
                                               "TimeToDoProductDetailing=N'{25}',TimeToImplementMerchandisingAction=N'{26}'," +
                                               "CheckInventoryToOrder=N'{27}',ExistingProductDetailing=N'{28}'," +
                                               "NewProductDetailing=N'{29}',Promotion=N'{30}'," +
                                               "Contract=N'{31}',MerchandisingPOPM=N'{32}'," +
                                               "HandlingIssues=N'{33}',OtherServices=N'{34}'," +
                                               "TimeToImplementActionsNegotiatedWithCustomer=N'{35}',TimeToInputIntoPOES=N'{36}',	" +
                                               "TotalStoreCallTime=N'{37}',SystemUsedToTakeOrder=N'{38}',	" +
                                               "TimingOfOrder=N'{39}',UpSelling=N'{40}',	" +
                                               "CrossSelling=N'{41}',OrderSuccessful=N'{42}',	" +
                                               "DeliveryTimeExpected=N'{43}',Comments=N'{44}'," +
                                               "WorkDayStartTime=N'{45}',WorkDayCompletedTime=N'{46}'," +
                                               "TotalWorkDaytime=N'{47}',LunchTime=N'{48}',NoonBreak=N'{49}',SpareTime=N'{50}' " +
            "where Id='{0}'", row["Id"], row["DateCallplan"], row["SC"], row["CustomerCode"], row["CustomerName"], 
            row["Address"], row["Dist"], row["Area"], row["CustomerClassification"], row["MonthlyCallFrequency"],
            row["CustomerType"], row["FFM"], row["ObserverName"],
            row["TransferTime"], row["TimeEntering"],
            row["MeetCustomer"], row["MeetPharmacyOwner"],
            row["MeetPharmacyOrderingStaff"], row["MeetPharmacyAssisstant"],
            row["TimeToGreet"], row["TimeToCheckStock"],
            row["TimeToTakeOrder"], row["TimeToNegotiate"],
            row["TimeToDoSurvey"], row["TimeToDiscussOtherServices"],
            row["TimeToDoProductDetailing"], row["TimeToImplementMerchandisingAction"],
            row["CheckInventoryToOrder"], row["ExistingProductDetailing"],
            row["NewProductDetailing"], row["Promotion"],
            row["Contract"], row["MerchandisingPOPM"],
            row["HandlingIssues"], row["OtherServices"],
            row["TimeToImplementActionsNegotiatedWithCustomer"],row["TimeToInputIntoPOES"],
            row["TotalStoreCallTime"],row["SystemUsedToTakeOrder"],
            row["TimingOfOrder"],row["UpSelling"],
            row["CrossSelling"],row["OrderSuccessful"],
            row["DeliveryTimeExpected"],row["Comments"],
            row["WorkDayStartTime"],row["WorkDayCompletedTime"],
            row["TotalWorkDaytime"],row["LunchTime"],
            row["NoonBreak"], row["SpareTime"]);

            return sqlHelper.UpdateData(updateQuery);
        }

        
        protected void ASPxGridView1_RowUpdating(object sender,
        DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DataRow row = ((DataTable)Session["GridData"]).Rows.Find(e.Keys["Id"]);
            row["DateCallplan"] = e.NewValues["DateCallplan"];
            row["SC"] = e.NewValues["SC"];
            row["CustomerCode"] = e.NewValues["CustomerCode"];
            row["CustomerName"] = e.NewValues["CustomerName"];
            row["Address"] = e.NewValues["Address"];
            row["Dist"] = e.NewValues["Dist"];
            row["Area"] = e.NewValues["Area"];
            row["CustomerClassification"] = e.NewValues["CustomerClassification"];
            row["MonthlyCallFrequency"] = e.NewValues["MonthlyCallFrequency"];
            row["CustomerType"] = e.NewValues["CustomerType"];
            row["FFM"] = e.NewValues["FFM"];
            row["ObserverName"] = e.NewValues["ObserverName"];

            row["TransferTime"] = e.NewValues["TransferTime"];
            row["TimeEntering"] = e.NewValues["TimeEntering"];
            row["MeetCustomer"] = e.NewValues["MeetCustomer"];
            row["MeetPharmacyOwner"] = e.NewValues["MeetPharmacyOwner"];
            row["MeetPharmacyOrderingStaff"] = e.NewValues["MeetPharmacyOrderingStaff"];
            row["MeetPharmacyAssisstant"] = e.NewValues["MeetPharmacyAssisstant"];
            row["TimeToGreet"] = e.NewValues["TimeToGreet"];
            row["TimeToCheckStock"] = e.NewValues["TimeToCheckStock"];
            row["TimeToTakeOrder"] = e.NewValues["TimeToTakeOrder"];
            row["TimeToNegotiate"] = e.NewValues["TimeToNegotiate"];
            row["TimeToDoSurvey"] = e.NewValues["TimeToDoSurvey"];
            row["TimeToDiscussOtherServices"] = e.NewValues["TimeToDiscussOtherServices"];
            row["TimeToDoProductDetailing"] = e.NewValues["TimeToDoProductDetailing"];
            row["TimeToImplementMerchandisingAction"] = e.NewValues["TimeToImplementMerchandisingAction"];
            row["CheckInventoryToOrder"] = e.NewValues["CheckInventoryToOrder"];
            row["ExistingProductDetailing"] = e.NewValues["ExistingProductDetailing"];
            row["NewProductDetailing"] = e.NewValues["NewProductDetailing"];
            row["Promotion"] = e.NewValues["Promotion"];
            row["Contract"] = e.NewValues["Contract"];
            row["MerchandisingPOPM"] = e.NewValues["MerchandisingPOPM"];
            row["HandlingIssues"] = e.NewValues["HandlingIssues"];
            row["OtherServices"] = e.NewValues["OtherServices"];

            row["TimeToImplementActionsNegotiatedWithCustomer"] = e.NewValues["TimeToImplementActionsNegotiatedWithCustomer"];
            row["TimeToInputIntoPOES"] = e.NewValues["TimeToInputIntoPOES"];
            row["TotalStoreCallTime"] = e.NewValues["TotalStoreCallTime"];
            row["SystemUsedToTakeOrder"] = e.NewValues["SystemUsedToTakeOrder"];
            row["TimingOfOrder"] = e.NewValues["TimingOfOrder"];
            row["UpSelling"] = e.NewValues["UpSelling"];
            row["CrossSelling"] = e.NewValues["CrossSelling"];
            row["OrderSuccessful"] = e.NewValues["OrderSuccessful"];
            row["DeliveryTimeExpected"] = e.NewValues["DeliveryTimeExpected"];
            row["Comments"] = e.NewValues["Comments"];

            row["WorkDayStartTime"] = e.NewValues["WorkDayStartTime"];
            row["WorkDayCompletedTime"] = e.NewValues["WorkDayCompletedTime"];
            row["TotalWorkDaytime"] = e.NewValues["TotalWorkDaytime"];
            row["LunchTime"] = e.NewValues["LunchTime"];
            row["NoonBreak"] = e.NewValues["NoonBreak"];
            row["SpareTime"] = e.NewValues["SpareTime"];

            UpdateData(row);
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            ASPxGridView1.DataBind();
        }

        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            gridExport.WriteXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }

        protected void btnBackHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "CustomerType")
            {
                //var sqlHelper = new SQLConnectionUtility();
                //string query = "SELECT distinct CustomerType FROM RouteRideChoseData";
                //var result = sqlHelper.GetData(query)

                //ASPxComboBox cmb = e.Editor as ASPxComboBox;
                //cmb.DataSource = result;
                //cmb.ValueField = "CustomerType";
                //cmb.ValueType = typeof(string);
                //cmb.TextField = "CustomerType";
                //cmb.DataBindItems();

                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "Pharmacy: Independent", "Pharmacy: Chain", "Hospital", 
                        "Clinic", "Wholesaler" }; ;
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                //cmb.ValueType = typeof (Int32);
                cmb.TextField = "";
                cmb.DataBindItems();
            }

            if ((e.Column.FieldName == "MeetCustomer")||
                (e.Column.FieldName == "MeetPharmacyOwner") ||
                (e.Column.FieldName == "MeetPharmacyOrderingStaff") ||
                (e.Column.FieldName == "MeetPharmacyAssisstant")||
                (e.Column.FieldName == "CheckInventoryToOrder") ||
                (e.Column.FieldName == "ExistingProductDetailing") ||
                (e.Column.FieldName == "NewProductDetailing") ||
                (e.Column.FieldName == "Promotion") ||
                (e.Column.FieldName == "Contract") ||
                (e.Column.FieldName == "MerchandisingPOPM") ||
                (e.Column.FieldName == "HandlingIssues") ||
                (e.Column.FieldName == "OtherServices")||
                (e.Column.FieldName == "UpSelling") ||
                (e.Column.FieldName == "CrossSelling") ||
                (e.Column.FieldName == "OrderSuccessful"))
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "Yes", "No"}; ;
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                //cmb.ValueType = typeof (Int32);
                cmb.TextField = "";
                cmb.DataBindItems();
            }

            if (e.Column.FieldName == "SystemUsedToTakeOrder")
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "POES", "Call center (by SC)", "Call center (by Cust)", "e-ZRx", "e-mail", "fax","Principal","" };
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                //cmb.ValueType = typeof (Int32);
                cmb.TextField = "";
                cmb.DataBindItems();
            }

            if (e.Column.FieldName == "TimingOfOrder")
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "During call", "Just after call", "Later during the day","" }; ;
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                //cmb.ValueType = typeof (Int32);
                cmb.TextField = "";
                cmb.DataBindItems();
            }

            if (e.Column.FieldName == "DeliveryTimeExpected")
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "Same day PM", "Next day AM", "Next day PM" }; ;
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                //cmb.ValueType = typeof (Int32);
                cmb.TextField = "";
                cmb.DataBindItems();
            }
        }









        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ((DataTable)Session["GridData"]).Rows.Add(new object[] { Guid.NewGuid(),
                            e.NewValues["Person"], e.NewValues["Age"] });
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_RowDeleting(object sender,
        DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            DataRow row = ((DataTable)Session["GridData"]).Rows.Find(e.Keys["ID"]);
            ((DataTable)Session["GridData"]).Rows.Remove(row);
            e.Cancel = true;
            ASPxGridView1.DataBind();
        }

    }
}