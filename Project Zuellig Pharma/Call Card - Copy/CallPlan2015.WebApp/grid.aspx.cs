using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

namespace CallPlan2015.WebApp
{
    public partial class grid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateGridView();
        }

        private void CreateGridView() {
            ASPxGridView gv = new ASPxGridView();
            gv.ID = "ASPxGridView1";
            gv.KeyFieldName = "UserCode"; 

            Page.Form.Controls.Add(gv);

            gv.SettingsBehavior.AllowFocusedRow = true;

            gv.DataSource = CreateData();
            gv.DataBind();
         }

        private SqlDataSource CreateData() {
            string conn = "Data Source=192.168.17.230;Initial Catalog=Zeris;user id=sa;password=P@ssw0rdz;Integrated Security=false";
            string selectCmnd = "SELECT UserCode,Password FROM UserCallPLan";
            return new SqlDataSource(conn, selectCmnd);
         }
         
    }
}