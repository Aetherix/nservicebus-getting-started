using NServiceBus;
using System;

namespace Shared
{
    public class SendPaymentFeedback : ICommand
    {
        public Guid OrderId { get; set; }
    }
}
