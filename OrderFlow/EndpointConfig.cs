using NServiceBus;

namespace OrderFlow
{
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public EndpointConfig()
        {
            global::NServiceBus.Logging.LogManager.Use<NLogFactory>();
        }

        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("MyTest.OrderFlow");
            configuration.EnableInstallers();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseSerialization<JsonSerializer>();
        }
    }
}
