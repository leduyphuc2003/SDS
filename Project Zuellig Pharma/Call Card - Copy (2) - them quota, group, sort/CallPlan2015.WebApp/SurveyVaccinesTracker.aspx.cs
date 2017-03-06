using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    public partial class SurveyVaccinesTracker : System.Web.UI.Page
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
                query = "SELECT * FROM SurveyVaccinesTracker";
            }
            else
            {
                query = string.Format("SELECT * FROM SurveyVaccinesTracker where SCcode='{0}'", user);
            }
            var result = sqlHelper.GetData(query);
            result.PrimaryKey = new DataColumn[] { result.Columns["Id"] };
            user = "";
            return result;
        }

        private bool UpdateData(DataRow row)
        {
            var sqlHelper = new SQLConnectionUtility();
            string updateQuery = string.Format("Update SurveyVaccinesTracker set STT=N'{1}',Vung=N'{2}'," +
                                               "Tinh=N'{3}',Code=N'{4}'," +
                                               "TenKhachHang=N'{5}',DoanhSoVaccineSubd=N'{6}'," +
                                               "DoanhSoVaccineZPV=N'{7}',TenNhaPhanPhoiDiaPhuongHienTai=N'{8}'," +
                                               "NhaPhanPhoiHienTai=N'{9}',ThanhToan1=N'{10}'," +
                                               "ThanhToan2=N'{11}',ThanhToan3=N'{12}'," +
                                               "MucDoThanThiet1=N'{13}',MucDoThanThiet2=N'{14}'," +
                                               "MucDoThanThiet3=N'{15}',ThoiGianGiaoHang=N'{16}'," +
                                               "ChietKhau1=N'{17}',ChietKhau2=N'{18}'," +
                                               "ChietKhau3=N'{19}',TaiTroGhiCuThe=N'{20}'," +
                                               "KhacVuiLongMoTa=N'{21}',KHNaoThichCtyDuocDiaPhuong=N'{22}'," +
                                               "RiskOfLPCsubD=N'{23}',FFMNote=N'{24}'," +
                                               "Manager=N'{25}',SC=N'{26}'," +
                                               "SCName=N'{27}' " +
            "where Id='{0}'", row["Id"], 
            row["STT"],row["Vung"],
            row["Tinh"],row["Code"],
            row["TenKhachHang"],row["DoanhSoVaccineSubd"],
            row["DoanhSoVaccineZPV"],row["TenNhaPhanPhoiDiaPhuongHienTai"],
            row["NhaPhanPhoiHienTai"],row["ThanhToan1"],
            row["ThanhToan2"],row["ThanhToan3"],
            row["MucDoThanThiet1"],row["MucDoThanThiet2"],
            row["MucDoThanThiet3"],row["ThoiGianGiaoHang"],
            row["ChietKhau1"],row["ChietKhau2"],
            row["ChietKhau3"],row["TaiTroGhiCuThe"],
            row["KhacVuiLongMoTa"],row["KHNaoThichCtyDuocDiaPhuong"],
            row["RiskOfLPCsubD"],row["FFMNote"],
            row["Manager"],row["SC"],
            row["SCName"]);

            return sqlHelper.UpdateData(updateQuery);
        }


        protected void ASPxGridView1_RowUpdating(object sender,
        DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DataRow row = ((DataTable)Session["GridData"]).Rows.Find(e.Keys["Id"]);
            //row["STT"] = e.NewValues["STT"];
            row["Vung"] = e.NewValues["Vung"];
            row["Tinh"] = e.NewValues["Tinh"];
            row["Code"] = e.NewValues["Code"];
            row["TenKhachHang"] = e.NewValues["TenKhachHang"];
            row["DoanhSoVaccineSubd"] = e.NewValues["DoanhSoVaccineSubd"];
            row["DoanhSoVaccineZPV"] = e.NewValues["DoanhSoVaccineZPV"];
            row["TenNhaPhanPhoiDiaPhuongHienTai"] = e.NewValues["TenNhaPhanPhoiDiaPhuongHienTai"];
            row["NhaPhanPhoiHienTai"] = e.NewValues["NhaPhanPhoiHienTai"];
            row["ThanhToan1"] = e.NewValues["ThanhToan1"];
            row["ThanhToan2"] = e.NewValues["ThanhToan2"];
            row["ThanhToan3"] = e.NewValues["ThanhToan3"];
            row["MucDoThanThiet1"] = e.NewValues["MucDoThanThiet1"];
            row["MucDoThanThiet2"] = e.NewValues["MucDoThanThiet2"];
            row["MucDoThanThiet3"] = e.NewValues["MucDoThanThiet3"];
            row["ThoiGianGiaoHang"] = e.NewValues["ThoiGianGiaoHang"];
            row["ChietKhau1"] = e.NewValues["ChietKhau1"];
            row["ChietKhau2"] = e.NewValues["ChietKhau2"];
            row["ChietKhau3"] = e.NewValues["ChietKhau3"];
            row["TaiTroGhiCuThe"] = e.NewValues["TaiTroGhiCuThe"];
            row["KhacVuiLongMoTa"] = e.NewValues["KhacVuiLongMoTa"];
            row["KHNaoThichCtyDuocDiaPhuong"] = e.NewValues["KHNaoThichCtyDuocDiaPhuong"];
            row["RiskOfLPCsubD"] = e.NewValues["RiskOfLPCsubD"];
            row["FFMNote"] = e.NewValues["FFMNote"];
            row["Manager"] = e.NewValues["Manager"];
            row["SC"] = e.NewValues["SC"];
            row["SCName"] = e.NewValues["SCName"];

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
            if (e.Column.FieldName == "NhaPhanPhoiHienTai")
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "ZPV", "Combination", "LPC" }; ;
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                //cmb.ValueType = typeof (Int32);
                cmb.TextField = "";
                cmb.DataBindItems();
            }

            if ((e.Column.FieldName == "ThanhToan1")||
                (e.Column.FieldName == "ThanhToan2")||
                (e.Column.FieldName == "ThanhToan3"))
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "30 D", "45 D", "60 D", "90 D", "COD", "Other" }; ;
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                //cmb.ValueType = typeof (Int32);
                cmb.TextField = "";
                cmb.DataBindItems();
            }

            if ((e.Column.FieldName == "MucDoThanThiet1")||
                (e.Column.FieldName == "MucDoThanThiet2")||
                (e.Column.FieldName == "MucDoThanThiet3"))
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "Very good", "Good", "Average", "Bad" }; ;
                cmb.ValueField = "";
                cmb.ValueType = typeof(string);
                //cmb.ValueType = typeof (Int32);
                cmb.TextField = "";
                cmb.DataBindItems();
            }

            if (e.Column.FieldName == "KHNaoThichCtyDuocDiaPhuong")
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = new[] { "X", "O" }; ;
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