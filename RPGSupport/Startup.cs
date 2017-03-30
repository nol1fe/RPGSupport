using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RPGSupport.Startup))]
namespace RPGSupport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
