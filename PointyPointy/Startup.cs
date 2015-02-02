using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PointyPointy.Startup))]
namespace PointyPointy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
