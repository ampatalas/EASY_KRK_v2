using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EASY_KRK.Startup))]
namespace EASY_KRK
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
