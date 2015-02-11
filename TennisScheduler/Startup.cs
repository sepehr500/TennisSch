using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TennisScheduler.Startup))]
namespace TennisScheduler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
