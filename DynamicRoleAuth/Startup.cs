using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DynamicRoleAuth.Startup))]
namespace DynamicRoleAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
