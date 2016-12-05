using NServiceBus;
using System;

namespace Shared
{
    public class PaymentFeedbackSent : IEvent
    {
        public Guid OrderId { get; set; }
    }
}
