using System;
using NServiceBus;

namespace Server
{
    public class EndpointConfig : IConfigureThisEndpoint, ISpecifyMessageHandlerOrdering
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("MyTest.Server");
            configuration.UseSerialization<JsonSerializer>();
            configuration.EnableInstallers();
            configuration.UsePersistence<InMemoryPersistence>();
        }

        public void SpecifyOrder(Order order)
        {
            order.SpecifyFirst<AuthenticationHandler>();
        }
    }
}
