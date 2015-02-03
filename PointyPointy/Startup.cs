using Microsoft.Owin;
using Owin;
using PointyPointy;

[assembly: OwinStartup(typeof (Startup))]

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