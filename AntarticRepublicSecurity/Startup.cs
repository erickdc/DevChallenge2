using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AntarticRepublicSecurity.Startup))]
namespace AntarticRepublicSecurity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
