using NServiceBus;
using System;

namespace Shared
{
    public class ShipmentCreated : IEvent
    {
        public Guid OrderId { get; set; }
    }
}
