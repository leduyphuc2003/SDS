using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallPlan2015.Common;
using CallPlan2015.Service;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;

namespace CallPlan2015.WebApp
{
    public partial class SurveyPolyclinicCustomerProfileUniverse : System.Web.UI.Page
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
            if (user == "adminsv")
            {
                query = "SELECT * FROM SurveyPolyclinicCustomerProfileUniverse";
            }
            else
            {
                query = string.Format("SELECT * FROM SurveyPolyclinicCustomerProfileUniverse where SCcode='{0}'", user);
            }
            var result = sqlHelper.GetData(query);
            result.PrimaryKey = new DataColumn[] { result.Columns["Id"] };
            user = "";
            return result;
        }



        private bool UpdateData(DataRow row)
        {
            var sqlHelper = new SQLConnectionUtility();
            string updateQuery = string.Format("Update SurveyPolyclinicCustomerProfileUniverse set No=N'{1}',State=N'{2}'," +
                                               "Province=N'{3}',AssignTEAM=N'{4}'," +
                                               "AssignASMOrMAM=N'{5}',SC=N'{6}'," +
                                               "AssignSC=N'{7}',CustomerCodePharmacyDept=N'{8}'," +
                                               "CustomerCodeServicePharmacy=N'{9}',Covered=N'{10}'," +
                                               "NamePrivateHospitalOrPolyclinic=N'{11}',CompanyName=N'{12}'," +
                                               "NumberOfBeds=N'{13}',ForeignInvestment=N'{14}'," +
                                               "SourceOfPurchase=N'{15}',Address=N'{16}'," +
                                               "Classification=N'{17}',KeyDecisionMaker=N'{18}'," +
                                               "Title=N'{19}',MobilePhone=N'{20}'," +
                                               "SpecialtyOrPoly=N'{21}',VaccinationRoom=N'{22}'," +
                                               "ContractWithGovernmentInsurance=N'{23}',NumberOfInsuranceCard=N'{24}'," +
                                               "ContractWithPrivateInsurances=N'{25}',CurrentCreditTerm=N'{26}'," +
                                               "CurrentCreditLimit=N'{27}',AveragePatientDaily=N'{28}'," +
                                               "TenderPriceApply=N'{29}',CurrentDeliveryFrequency=N'{30}'," +
                                               "LoyaltyProgramOrPromotion=N'{31}',Remark=N'{32}'," +
                                               "FFMFeedback=N'{33}',TeamT=N'{34}'," +
                                               "StateT=N'{35}',NewOldList=N'{36}'" +
            "where Id='{0}'", row["Id"], row["No"], row["State"],
            row["Province"], row["AssignTEAM"],
            row["AssignASMOrMAM"], row["SC"],
            row["AssignSC"], row["CustomerCodePharmacyDept"],
            row["CustomerCodeServicePharmacy"], row["Covered"],
            row["NamePrivateHospitalOrPolyclinic"], row["CompanyName"],
            row["NumberOfBeds"], row["ForeignInvestment"],
            row["SourceOfPurchase"], row["Address"],
            row["Classification"], row["KeyDecisionMaker"],
            row["Title"], row["MobilePhone"],
            row["SpecialtyOrPoly"], row["VaccinationRoom"],
            row["ContractWithGovernmentInsurance"], row["NumberOfInsuranceCard"],
            row["ContractWithPrivateInsurances"], row["CurrentCreditTerm"],
            row["CurrentCreditLimit"], row["AveragePatientDaily"],
            row["TenderPriceApply"], row["CurrentDeliveryFrequency"],
            row["LoyaltyProgramOrPromotion"], row["Remark"],
            row["FFMFeedback"], row["TeamT"],
            row["StateT"], row["NewOldList"]);
            return sqlHelper.UpdateData(updateQuery);
        }


        protected void ASPxGridView1_RowUpdating(object sender,
        DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DataRow row = ((DataTable)Session["GridData"]).Rows.Find(e.Keys["Id"]);
            //row["No"] = e.NewValues["No"];
            row["State"] = e.NewValues["State"];
            row["Province"] = e.NewValues["Province"];
            row["AssignTEAM"] = e.NewValues["AssignTEAM"];
            row["AssignASMOrMAM"] = e.NewValues["AssignASMOrMAM"];
            row["SC"] = e.NewValues["SC"];
            row["AssignSC"] = e.NewValues["AssignSC"];
            row["CustomerCodePharmacyDept"] = e.NewValues["CustomerCodePharmacyDept"];
            row["CustomerCodeServicePharmacy"] = e.NewValues["CustomerCodeServicePharmacy"];
            row["Covered"] = e.NewValues["Covered"];
            row["NamePrivateHospitalOrPolyclinic"] = e.NewValues["NamePrivateHospitalOrPolyclinic"];
            row["CompanyName"] = e.NewValues["CompanyName"];
            row["NumberOfBeds"] = e.NewValues["NumberOfBeds"];
            row["ForeignInvestment"] = e.NewValues["ForeignInvestment"];
            row["SourceOfPurchase"] = e.NewValues["SourceOfPurchase"];
            row["Address"] = e.NewValues["Address"];
            row["Classification"] = e.NewValues["Classification"];
            row["KeyDecisionMaker"] = e.NewValues["KeyDecisionMaker"];
            row["Title"] = e.NewValues["Title"];
            row["MobilePhone"] = e.NewValues["MobilePhone"];
            row["SpecialtyOrPoly"] = e.NewValues["SpecialtyOrPoly"];
            row["VaccinationRoom"] = e.NewValues["VaccinationRoom"];
            row["ContractWithGovernmentInsurance"] = e.NewValues["ContractWithGovernmentInsurance"];
            row["NumberOfInsuranceCard"] = e.NewValues["NumberOfInsuranceCard"];
            row["ContractWithPrivateInsurances"] = e.NewValues["ContractWithPrivateInsurances"];
            row["CurrentCreditTerm"] = e.NewValues["CurrentCreditTerm"];
            row["CurrentCreditLimit"] = e.NewValues["CurrentCreditLimit"];
            row["AveragePatientDaily"] = e.NewValues["AveragePatientDaily"];
            row["TenderPriceApply"] = e.NewValues["TenderPriceApply"];
            row["CurrentDeliveryFrequency"] = e.NewValues["CurrentDeliveryFrequency"];
            row["LoyaltyProgramOrPromotion"] = e.NewValues["LoyaltyProgramOrPromotion"];
            row["Remark"] = e.NewValues["Remark"];
            row["FFMFeedback"] = e.NewValues["FFMFeedback"];
            row["TeamT"] = e.NewValues["TeamT"];
            row["StateT"] = e.NewValues["StateT"];
            row["NewOldList"] = e.NewValues["NewOldList"];

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
            if (e.Column.FieldName == "State")
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "North Region", "Central Region", "South Region", "Mekong Region", "HCM","Ha Noi" };
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                cmb.TextField = "";
                cmb.DataBindItems();
            }
 
            if (e.Column.FieldName == "SourceOfPurchase")
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "ZPV/Wholesalers", "ZPV/Wholesalers/Other Distributions", "Wholesalers/Other Distributions", "ZPV/Other Distributions" }; ;
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                cmb.TextField = "";
                cmb.DataBindItems();
            }

            if (e.Column.FieldName == "Classification")
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "Private Hos", "Polyclinic"};
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                cmb.TextField = "";
                cmb.DataBindItems();
            }

            if (e.Column.FieldName == "Title")
            {
 

                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] {"Giám đốc", "Phó giám đốc", "Trưởng khoa dược", "Hội đồng quản trị", "Khác" }; ;
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                cmb.TextField = "";
                cmb.DataBindItems();
            }

            if (e.Column.FieldName == "SpecialtyOrPoly")
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "Đa khoa", "Sản", "Nhi", "Thần Kinh", "Cơ Xương Khớp", "Chuyên khoa khác" }; ;
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                cmb.TextField = "";
                cmb.DataBindItems();
            }

            if ((e.Column.FieldName == "VaccinationRoom") || (e.Column.FieldName == "ContractWithGovernmentInsurance")
                || (e.Column.FieldName == "ContractWithPrivateInsurances")
                || (e.Column.FieldName == "ForeignInvestment"))
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "Yes","No" }; ;
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                cmb.TextField = "";
                cmb.DataBindItems();
            }

            if (e.Column.FieldName == "Remark")
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[]
                {
                    "Got code/KH da co code", "No code/KH chua mo code",
                    "No code_Provide License/KH cung cap giay phep","No code_Not provide License/KH khong cung cap giay phep",
                    "Closed code/KH dong cua/dong code","Herbal Medicine Clinic/PK Y hoc co truyen",
                    "Have no demand to open code/KH khong co nhu cau mo code","Other/Khac"
                }; ;
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
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