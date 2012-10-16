using System;
using HelloWorld.Common;
using RabbitBus;

namespace HelloWorld.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Bus bus = new BusBuilder().Build();
            bus.Connect();

            bool readingInput = true;
            while (readingInput)
            {
                Console.WriteLine("Press enter to publish a message or 'X' to exit");

                var keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        string messageText = "Hello, World";
                        bus.Publish(new Message(messageText));
                        Console.WriteLine("Published message: {0}", messageText);
                        break;

                    case ConsoleKey.X:
                        readingInput = false;
                        break;
                }
            }

            bus.Close();
        }
    }
}