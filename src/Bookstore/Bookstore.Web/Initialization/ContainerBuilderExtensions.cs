using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Core;

namespace Bookstore.Web.Initialization
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterModulesFor(this ContainerBuilder builder, Assembly assembly)
        {
            var scanningBuilder = new ContainerBuilder();

            scanningBuilder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(IModule).IsAssignableFrom(t))
                .As<IModule>();

            using (IContainer scanningContainer = scanningBuilder.Build())
            {
                foreach (IModule m in scanningContainer.Resolve<IEnumerable<IModule>>())
                    builder.RegisterModule(m);
            }

            return builder;
        }
    }
}