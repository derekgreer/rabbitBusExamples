using System;
using RabbitBus;
using RabbitBus.Serialization.Json;
using SubscriptionConventions.Service;

namespace SubscriptionConventions.Producer
{
	class Program
	{
		static void Main(string[] args)
		{
			Bus bus = new BusBuilder().Configure(ctx => ctx.WithDefaultSerializationStrategy(new JsonSerializationStrategy())).Build();
			bus.Connect();

			bool readingInput = true;
			while(readingInput)
			{
				Console.WriteLine("Press enter to publish a message or 'X' to exit");
				
				var keyInfo = Console.ReadKey();

				switch(keyInfo.Key)
				{
					case ConsoleKey.Enter:
						bus.Publish(new StatusMessage("This is a test."));
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