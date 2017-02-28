using System;
using CallPlan2015.Common;
using CallPlan2015.WebApp.Common;

namespace CallPlan2015.WebApp
{
	public partial class SiteMaster : BaseMasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				hfMenuActive.Value = Request.Url.AbsoluteUri;
			}
		}

	    protected void LogOut(object sender, EventArgs e)
	    {
	        Session[Constants.SESSION_PHARMACY_CUSTOMER_LIST] = null;
	        Session[Constants.SESSION_USER_NAME] = null;

            Session[Constants.SESSION_PHARMACY_DATE] = null;
            Session[Constants.SESSION_PHARMACY_DATE_SHOW] = null;

            Session[Constants.SESSION_GL_Pharmacy_DATE] = null;
            Session[Constants.SESSION_GL_Pharmacy_DATE_SHOW] = null;

            Session[Constants.SESSION_GOV_HOS_DATE] = null;
            Session[Constants.SESSION_GOV_HOS_DATE_SHOW] = null;

            Session[Constants.SESSION_PRI_HOS_DATE] = null;
            Session[Constants.SESSION_PRI_HOS_DATE_SHOW] = null;

            Session[Constants.SESSION_EXCLUSIVE_DATE] = null;
            Session[Constants.SESSION_EXCLUSIVE_DATE_SHOW] = null;

            Response.Redirect(RedirectUrl("~/Login.aspx"));
	    }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect(RedirectUrl("~/Default.aspx"));
        }
        protected void btnAbout_Click(object sender, EventArgs e)
        {
            Response.Redirect(RedirectUrl("~/About.aspx"));
        } 

	}
}
