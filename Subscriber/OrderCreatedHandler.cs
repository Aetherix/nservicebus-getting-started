using NServiceBus;
using NServiceBus.Logging;
using Shared;

namespace Subscriber
{
    public class SendPaymentFeedbackHandler : IHandleMessages<SendPaymentFeedback>
    {
        static ILog log = LogManager.GetLogger<SendPaymentFeedbackHandler>();

        public void Handle(SendPaymentFeedback message)
        {
            log.Info($"Handling: SendPaymentFeedback for Order Id: {message.OrderId}");
        }
    }
}
