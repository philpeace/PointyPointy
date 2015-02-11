using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using CodePeace.Common.Configuration;

namespace PointyPointy
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            var container = ContainerConfig.BuildContainer(builder);
            var appSettingsConfigManager = container.Resolve<IAppSettingsConfigurationManager>();
            var enableOptimizations = appSettingsConfigManager.Setting<bool>("Bundling.EnableOptimizations");

            BundleTable.EnableOptimizations = enableOptimizations;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles, BundleTable.EnableOptimizations);
        }
    }
}