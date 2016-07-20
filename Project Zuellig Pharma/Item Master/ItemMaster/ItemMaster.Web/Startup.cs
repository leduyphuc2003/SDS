using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ItemMaster.Web.Startup))]
namespace ItemMaster.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
