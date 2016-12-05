using System;
using NServiceBus;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Subscriber";
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("MyTest.Subscriber");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            using (var bus = Bus.Create(busConfiguration).Start())
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
