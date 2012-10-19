using System;
using System.Diagnostics;
using System.Threading;
using RabbitBus;
using RabbitBus.Configuration;
using RabbitMQ.Client;

namespace RabbitBusSpeedTest
{
    class Program
    {
        static int _messageReceived;
        static int _rabbitMqMessageReceived;


        static void Main(string[] args)
        {
            const int testCount = 10000;

            ConsumeRabbitBusMessages(testCount);
            ConsumeRabbitMqMessages(testCount);

            Console.WriteLine("Testing with " + testCount + " messages");
            TimeSpan elapsed = Time(() => TestRabbitBus(testCount));
            Console.WriteLine("RabbitBus time elapsed: {0} - average message: {1}", elapsed.TotalSeconds,
                              elapsed.TotalSeconds/testCount);
            elapsed = Time(() => TestRabbitMq(testCount));
            Console.WriteLine("RabbitMQ time elapsed: {0} - average message: {1}", elapsed.TotalSeconds,
                              elapsed.TotalSeconds/testCount);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static void ConsumeRabbitBusMessages(int testCount)
        {
            new Thread(() =>
                {
                    var reset = new ManualResetEvent(false);
                    Bus bus = new BusBuilder()
                        .Configure(ctx => ctx.Consume<TestMessage>().WithExchange("rabbitBus.test.exchange")
                                              .WithQueue("rabbitBus.test.queue"))
                        .Build();
                    bus.Connect();
                    bus.Subscribe<TestMessage>(x =>
                        {
                            _messageReceived++;
                            if (_messageReceived >= testCount)
                                reset.Set();
                        });

                    reset.WaitOne();
                    Console.WriteLine("Finished consuming rabbitBus messages: " + _messageReceived);
                    bus.Close();
                }).Start();
        }

        static void ConsumeRabbitMqMessages(int testCount)
        {
            new Thread(() =>
                {
                    var reset = new ManualResetEvent(false);
                    Bus bus = new BusBuilder()
                        .Configure(ctx => ctx.Consume<TestMessage>().WithExchange("rabbitMQ.test.exchange")
                                              .WithQueue("rabbitMQ.test.queue"))
                        .Build();
                    bus.Connect();
                    bus.Subscribe<TestMessage>(x =>
                        {
                            _rabbitMqMessageReceived++;
                            if (_rabbitMqMessageReceived >= testCount)
                                reset.Set();
                        });

                    reset.WaitOne();
                    Console.WriteLine("Finished consuming rabbitMQ messages: " + _rabbitMqMessageReceived);
                    bus.Close();
                }).Start();
        }

        static TimeSpan Time(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        static void TestRabbitBus(int messageCount)
        {
            Bus bus = new BusBuilder()
                .Configure(ctx => ctx.Publish<TestMessage>().WithExchange("rabbitBus.test.exchange"))
                .Build();
            bus.Connect();
            for (int i = 0; i < messageCount; i++)
            {
                bus.Publish(new TestMessage("test"));
            }
            bus.Close();
        }

        static void TestRabbitMq(int messageCount)
        {
            IConnection connection = new ConnectionFactory().CreateConnection();

            var serializer = new BinarySerializationStrategy();

            for (int i = 0; i < messageCount; i++)
            {
                IModel model = connection.CreateModel();
                model.ExchangeDeclare("rabbitMQ.test.exchange", ExchangeType.Direct, false, true, null);
                byte[] serializedMessage = serializer.Serialize(new TestMessage("test"));
                model.BasicPublish("rabbitMQ.test.exchange", string.Empty, null, serializedMessage);
                model.Close();
            }

            connection.Close();
        }
    }
}