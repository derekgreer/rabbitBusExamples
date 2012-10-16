using System;
using Autofac;
using RabbitBus.Configuration;

namespace SubscriptionConventions.Service
{
	class AutofacDependencyResolver : IDependencyResolver
	{
		readonly IComponentContext _componentContext;

		public AutofacDependencyResolver(IComponentContext componentContext)
		{
			_componentContext = componentContext;
		}

		public object Resolve(Type handlerType)
		{
			return _componentContext.Resolve(handlerType);
		}
	}
}