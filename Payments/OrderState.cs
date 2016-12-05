using System;
using NServiceBus.Saga;

namespace Payments
{
    public class OrderState : IContainSagaData
    {
        public Guid Id { get; set; }
        public MessageStatus PaymentFeedbackStatus { get; set; }
        public MessageStatus ShipmentStatus { get; set; }
        public Guid OrderId { get; set; }
        public string OriginalMessageId { get; set; }
        public string Originator { get; set; }
    }
}
