using System;
using NServiceBus;

namespace Server
{
    public class EndpointConfig : IConfigureThisEndpoint, ISpecifyMessageHandlerOrdering
    {
        public EndpointConfig()
        {
            global::NServiceBus.Logging.LogManager.Use<NLogFactory>();
        }

        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("MyTest.Server");
            configuration.EnableInstallers();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseSerialization<JsonSerializer>();
        }

        public void SpecifyOrder(Order order)
        {
            order.SpecifyFirst<AuthenticationHandler>();
        }
    }
}
