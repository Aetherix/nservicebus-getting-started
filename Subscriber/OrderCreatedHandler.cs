using NServiceBus;
using NServiceBus.Logging;
using Shared;

namespace Subscriber
{
    public class OrderCreatedHandler : IHandleMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderCreatedHandler>();

        public void Handle(OrderPlaced message)
        {
            log.Info($"Handling: OrderPlaced for Order Id: {message.OrderId}");
        }
    }
}
