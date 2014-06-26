using Microsoft.Owin;
using MyMediaCatalog;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace MyMediaCatalog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}