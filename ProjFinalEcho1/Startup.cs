using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjFinalEcho1.Startup))]
namespace ProjFinalEcho1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
