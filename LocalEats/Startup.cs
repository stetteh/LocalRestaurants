using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocalEats.Startup))]
namespace LocalEats
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
