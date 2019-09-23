using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ap_Financial_Portal.Startup))]
namespace Ap_Financial_Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
