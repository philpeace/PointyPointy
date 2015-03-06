using System;
using System.Configuration;
using System.Reflection;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using CodePeace.Common;
using CodePeace.Common.Web;
using PointyPointy.Data;
using PointyPointy.Data.Contexts;
using PointyPointy.Data.DataAccess;
using PointyPointy.Data.Entities;

namespace PointyPointy
{
    public static class ContainerConfig
    {
        public static IContainer BuildContainer(ContainerBuilder builder)
        {
            var assemblies = System.Web.Compilation.BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();
            //Assembly[] assembliess = AppDomain.CurrentDomain.GetAssemblies();

            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterType<ExtensibleActionInvoker>().As<IActionInvoker>();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof (IDependency).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<PointyContext>().As<IDAOContext>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(DAO<>)).As(typeof(IDAO<>));

            builder.RegisterType<PointyContext>().As<IPointyContext>()
                    .WithParameter(new TypedParameter(typeof(string), ConfigurationManager.ConnectionStrings["PointyContext"].ToString()))
                    .InstancePerRequest();

            builder.RegisterType<GenericRepository<ScrumInvite>>().As<IRepository<ScrumInvite>>()
                   .WithParameter(new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IDbContext),
                        (p, c) => DependencyResolver.Current.GetService<IPointyContext>())).InstancePerRequest();

            builder.RegisterType<GenericRepository<ScrumInviteUser>>().As<IRepository<ScrumInviteUser>>()
                   .WithParameter(new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IDbContext),
                        (p, c) => DependencyResolver.Current.GetService<IPointyContext>())).InstancePerRequest();

            // Set the dependency resolver to be Autofac.
            builder.RegisterControllers(assemblies).InjectActionInvoker();
            builder.RegisterModelBinders(assemblies);
            builder.RegisterModelBinderProvider();
            builder.RegisterFilterProvider();
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }
    }
}