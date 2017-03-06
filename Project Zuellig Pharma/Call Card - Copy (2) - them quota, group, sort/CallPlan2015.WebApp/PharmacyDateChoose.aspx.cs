﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallPlan2015.Common;
using CallPlan2015.Service;

namespace CallPlan2015.WebApp
{
    public partial class PharmacyDateChoose : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox1.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }
            
        }
        protected void btnClick(object sender, EventArgs e)
        {
            //if (TextBox1.Text == "")
            //{
            //    lblDate.Text = "Chon ngay de xem !";
            //}
            //else
            //{ 
                try
                {
                    DateTime dt = DateTime.ParseExact(TextBox1.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    Session[Constants.SESSION_PHARMACY_DATE_SHOW] = dt.ToString("dd/MM/yyyy");
                    Session[Constants.SESSION_PHARMACY_DATE] = dt.ToString("yyyyMMdd");
                    lblDate.Text = dt.ToString("yyyyMMdd");
                    Response.Redirect("~/Forms/CallPlanByCus.aspx");
                }
                catch (Exception)
                {
                    lblDate.Text = "Please choose correct date !";
                }

            //}
            
        }
    }
}