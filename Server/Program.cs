using System;
using NServiceBus;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Server";
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("MyTest.Server");
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
