using NLog;
using NServiceBus;
using Shared;

namespace Server
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        private static readonly Logger s_log = LogManager.GetCurrentClassLogger();

        IBus bus;

        public PlaceOrderHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(PlaceOrder message)
        {
            s_log.Info($"Order for Product:{message.Product} placed with id: {message.Id}");
            s_log.Info($"Publishing: OrderPlaced for Order Id: {message.Id}");

            var orderPlaced = new OrderPlaced
            {
                OrderId = message.Id
            };
            bus.Publish(orderPlaced);
        }
    }
}
