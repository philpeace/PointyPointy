using Microsoft.Owin;
using Owin;
using PointyPointy;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace PointyPointy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.UseHangfire(config =>
            {
                config.UseSqlServerStorage("Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\aspnet-PointyPointy-20150126090010.mdf;Initial Catalog=aspnet-PointyPointy-20150126090010;Integrated Security=True;User Instance=True");
                config.UseServer();
            });
        }
    }
}