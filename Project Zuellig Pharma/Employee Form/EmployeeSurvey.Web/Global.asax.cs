using EmployeeSurvey.Web.Migrations;
using EmployeeSurvey.Web.Models;
using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EmployeeSurvey.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        /// <summary>
        /// Action will run before filter execute
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void Application_AcquireRequestState(object sender, EventArgs eventArgs)
        {
            
        }
    }
}
