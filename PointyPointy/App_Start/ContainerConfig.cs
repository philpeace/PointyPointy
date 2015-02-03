using System;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using CodePeace.Common;

namespace PointyPointy
{
    public static class ContainerConfig
    {
        public static void BuildContainer(ContainerBuilder builder)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterType<ExtensibleActionInvoker>().As<IActionInvoker>();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof (IDependency).IsAssignableFrom(t))
                .AsImplementedInterfaces();

            // Set the dependency resolver to be Autofac.
            builder.RegisterControllers(assemblies).InjectActionInvoker();
            builder.RegisterModelBinders(assemblies);
            builder.RegisterModelBinderProvider();
            builder.RegisterFilterProvider();
            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}