using System.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeeSurvey.Web.Startup))]
namespace EmployeeSurvey.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
