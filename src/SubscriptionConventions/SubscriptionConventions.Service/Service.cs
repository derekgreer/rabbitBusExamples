using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using Autofac;
using RabbitBus;
using RabbitBus.Configuration;
using RabbitBus.Serialization.Json;

namespace SubscriptionConventions.Service
{
	public class Service
	{
		static IContainer _context;
		IBus _bus;

		public void Start()
		{
			_context = CreateContext();
			_bus = _context.Resolve<IBus>();
			ServiceLogger.Current.Write("Starting Service ...", TraceEventType.Start);
		}

		public void Stop()
		{
			IServiceLogger logger = ServiceLogger.Current;
			logger.Write("Disposing _context ...", TraceEventType.Information);
			_context.Dispose();
			logger.Write("Disposed _context.", TraceEventType.Information);
			logger.Write("Service stopped.", TraceEventType.Stop);
		}

		IContainer CreateContext()
		{
			if (_context == null)
			{
				var builder = new ContainerBuilder();
				builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsSelf().AsImplementedInterfaces();
				builder.Register(ctx => CreateRabbitBus()).As<IBus>().SingleInstance()
					.OnActivated(args => ((Bus) args.Instance)
					                     	.Connect(
					                     		ConfigurationManager.ConnectionStrings["amqp"].ConnectionString,
					                     		TimeSpan.FromMinutes(5)))
					.OnRelease(bus => ((Bus) bus).Close());
				_context = builder.Build();
			}

			ServiceLogger.SetCurrentLogger(new Log4NetLogger());

			return _context;
		}


		static IBus CreateRabbitBus()
		{
			return new BusBuilder()
				.Configure(ctx => ctx
				                  	.WithDefaultSerializationStrategy(new JsonSerializationStrategy())
				                  	.WithLogger(_context.Resolve<RabbitBusLoggerAdapter>()))
				.Configure(new AutoConfigurationModelBuilder()
				           	.WithCallingAssembly()
				           	.WithDefaultConventions()
										.WithDependencyResolver(_context.Resolve<AutofacDependencyResolver>())
				           	.Build())
				.Build();
		}
	}
}