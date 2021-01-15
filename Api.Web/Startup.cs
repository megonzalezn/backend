using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Api.Web.Startup))]
namespace Api.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
