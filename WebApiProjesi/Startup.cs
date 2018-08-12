using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApiProjesi.Startup))]

namespace WebApiProjesi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}