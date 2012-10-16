using System;
using System.IO;
using System.Reflection;
using Topshelf;
using Topshelf.Logging;
using log4net.Config;

namespace SubscriptionConventions.Service
{
	class Program
	{
		const string Log4NetConfig = ".\\log4net.config";

		static void Main()
		{
			Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			XmlConfigurator.ConfigureAndWatch(new FileInfo(Log4NetConfig));
			HostLogger.UseLogger(new Log4NetLogWriterFactory.Log4NetLoggerConfigurator(Log4NetConfig));

			HostFactory.Run(x =>
				{
					x.Service<Service>(s =>
						{
							s.ConstructUsing(() => new Service());
							s.WhenStarted(service => service.Start());
							s.WhenStopped(service => service.Stop());
						});

					x.RunAsLocalSystem();
					x.SetDescription("SubscriptionConventions example service.");
					x.SetServiceName("SubscriptionConventionsExampleService");
					x.SetDisplayName("SubscriptionConventions Example Service");
				});
		}
	}
}