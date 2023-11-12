using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SupriMaster.AppMvc.Startup))]
namespace SupriMaster.AppMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
