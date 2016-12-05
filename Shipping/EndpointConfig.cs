using NServiceBus;

namespace Shipping
{
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public EndpointConfig()
        {
            global::NServiceBus.Logging.LogManager.Use<NLogFactory>();
        }

        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("MyTest.Shipping");
            configuration.EnableInstallers();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseSerialization<JsonSerializer>();
        }
    }
}
