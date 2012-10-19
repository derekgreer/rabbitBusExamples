using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using Module = Autofac.Module;

namespace Bookstore.Web.Initialization
{
    public class BookstoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(executingAssembly);

            IEnumerable<Assembly> assemblies = GetRelatedAssemblies(executingAssembly);
            assemblies.ToList().ForEach(assembly => builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces());
        }

        IEnumerable<Assembly> GetRelatedAssemblies(Assembly executingAssembly)
        {
            yield return executingAssembly;

            foreach (AssemblyName relatedAssemblyName in executingAssembly.GetReferencedAssemblies()
                .Where(name => name.Name.StartsWith(GetThisAssembliesPrefix())))
            {
                yield return Assembly.Load(relatedAssemblyName);
            }
        }

        string GetThisAssembliesPrefix()
        {
            string name = GetType().Assembly.GetName().Name;
            name = name.Substring(0, name.LastIndexOf(".", StringComparison.Ordinal));
            return name;
        }
    }
}