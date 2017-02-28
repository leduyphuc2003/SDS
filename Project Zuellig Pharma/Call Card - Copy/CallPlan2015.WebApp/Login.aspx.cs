using System;
using CallPlan2015.Common;
using CallPlan2015.Service;

namespace CallPlan2015.WebApp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {

            var sqlHelper = new SQLConnectionUtility();
            var user = sqlHelper.GetUser(txtUserName.Text, txtPassword.Text);

            Session[Constants.SESSION_USER_NAME] = null;
            if (user != string.Empty)
            {
                Session[Constants.SESSION_USER_NAME] = user;
                Response.Redirect(ResolveUrl("~/Default.aspx"));
            }

            errorMessage.Visible = true;
        }
    }
}