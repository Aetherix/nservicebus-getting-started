using NServiceBus;

namespace Payments
{
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public EndpointConfig()
        {
            global::NServiceBus.Logging.LogManager.Use<NLogFactory>();
        }

        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("MyTest.Payments");
            configuration.EnableInstallers();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseSerialization<JsonSerializer>();
        }
    }
}
