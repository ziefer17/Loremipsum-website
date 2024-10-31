using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectLoremipsu.Startup))]
namespace ProjectLoremipsu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
