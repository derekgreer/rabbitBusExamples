using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace Bookstore.Web.Initialization
{
    public class Bootstrapper
    {
        IContainer _container;

        public void Run()
        {
            ConfigureContainer();
            InitializeComponents();
            InitializeServiceLocators();
        }

        void InitializeComponents()
        {
            var initializers = _container.Resolve<IEnumerable<IApplicationInitializer>>();
            initializers.ToList().ForEach(initializer => initializer.Initialize());
        }

        void ConfigureContainer()
        {
            _container = new ContainerBuilder().RegisterModulesFor(GetType().Assembly).Build();
        }

        void InitializeServiceLocators()
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }
    }
}