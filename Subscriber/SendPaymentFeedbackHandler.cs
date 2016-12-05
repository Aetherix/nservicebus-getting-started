using NServiceBus;
using NServiceBus.Logging;
using Shared;
using System.Threading;

namespace Subscriber
{
    public class SendPaymentFeedbackHandler : IHandleMessages<SendPaymentFeedback>
    {
        static ILog log = LogManager.GetLogger<SendPaymentFeedbackHandler>();

        public IBus Bus { get; set; }

        public void Handle(SendPaymentFeedback message)
        {
            log.Info($"Handling: SendPaymentFeedback for Order Id: {message.OrderId}");

            // Thread.Sleep(10000);

            Bus.Publish<PaymentFeedbackSent>(x => { x.OrderId = message.OrderId; });
        }
    }
}
