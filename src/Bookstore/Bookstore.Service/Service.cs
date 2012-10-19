using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using Autofac;
using Bookstore.Common;
using RabbitBus;
using RabbitBus.Serialization.Json;

namespace Bookstore.Service
{
    public class Service
    {
        static IContainer _context;

        public void Start()
        {
            _context = CreateContext();
            var bus = _context.Resolve<IBus>();
            bus.Subscribe<BookOrder>(new BookOrderHandler().Handle);
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
                    .OnActivated(
                        args =>
                        ((Bus) args.Instance).Connect(ConfigurationManager.ConnectionStrings["amqp"].ConnectionString,
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
                                      .WithLogger(_context.Resolve<RabbitBusLoggerAdapter>())
                                      .Consume<BookOrder>()
                                      .WithExchange("bookstore-exchange", cfg => cfg.Direct().Durable().Not.AutoDelete())
                                      .WithQueue("bookstore-queue", cfg => cfg.Durable().Not.AutoDelete()))
                .Build();
        }
    }

    public class BookOrderHandler
    {
        public void Handle(IMessageContext<BookOrder> messageContext)
        {
            ServiceLogger.Current.Write("Processing order for book: " + messageContext.Message.BookName);
            messageContext.AcceptMessage();
        }
    }
}