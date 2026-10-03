using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SGTB.Startup))]
namespace SGTB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
