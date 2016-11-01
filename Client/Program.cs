using System;
using NServiceBus;
using Shared;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client";
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("MyTest.Client");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            using (IBus bus = Bus.Create(busConfiguration).Start())
            {
                SendOrder(bus);
            }
        }

        static void SendOrder(IBus bus)
        {
            Console.WriteLine("Press enter to send a message");
            Console.WriteLine("Press any key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key != ConsoleKey.Enter)
                {
                    return;
                }
                var id = Guid.NewGuid();

                var placeOrder = new PlaceOrder
                {
                    Product = "New shoes",
                    Id = id
                };
                bus.Send("MyTest.Server", placeOrder);
                Console.WriteLine($"Sent a new PlaceOrder message with id: {id.ToString("N")}");
            }
        }
    }
}
