using System.Configuration;
using Autofac;
using Bookstore.Common;
using RabbitBus;
using RabbitBus.Serialization.Json;

namespace Bookstore.Web.Initialization
{
    public class RabbitBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(componentContext =>
                          new BusBuilder()
                              .Configure(ctx => ctx.WithDefaultSerializationStrategy(new JsonSerializationStrategy())
                                                    .Publish<BookOrder>()
                                                    .WithExchange("bookstore-exchange", cfg => cfg.Direct().Durable().Not.AutoDelete()))
                              .Build())
                .As<IBus>().SingleInstance()
                .OnActivated(args => args.Instance.Connect(ConfigurationManager.ConnectionStrings["amqp"].ConnectionString))
                .OnRelease(bus => bus.Close());
        }
    }
}