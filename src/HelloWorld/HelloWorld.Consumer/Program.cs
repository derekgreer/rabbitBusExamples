using System;
using HelloWorld.Common;
using RabbitBus;

namespace HelloWorld.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Bus bus = new BusBuilder().Build();
            bus.Connect();

            bus.Subscribe<Message>(messageContext => Console.WriteLine("Received message: \'{0}\'", messageContext.Message.Text));
            
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            bus.Close();
        }
    }
}