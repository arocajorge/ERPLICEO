using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Core.Erp.Web.Startup))]
namespace Core.Erp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
