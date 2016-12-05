using NServiceBus;
using Shared;
using NServiceBus.Logging;

namespace Shipping
{
    public class CreateShipmentHandler : IHandleMessages<CreateShipment>
    {
        static ILog log = LogManager.GetLogger<CreateShipmentHandler>();

        public IBus Bus { get; set; }

        public void Handle(CreateShipment message)
        {
            log.Info($"Handling: CreateShipment for Order Id: {message.OrderId}");

            Bus.Publish<ShipmentCreated>(x => { x.OrderId = message.OrderId; });
        }
    }
}
