using NServiceBus.Saga;
using Shared;
using NLog;
using System;

namespace Payments
{
    public class PaymentSaga : Saga<OrderState>, IAmStartedByMessages<OrderPlaced>, IHandleTimeouts<PaymentFeedbackTimeout>
    {
        private static readonly Logger s_log = LogManager.GetCurrentClassLogger();

        public void Handle(OrderPlaced message)
        {
            Data.OrderId = message.OrderId;

            RequestTimeout<PaymentFeedbackTimeout>(TimeSpan.FromSeconds(5));

            Bus.Send(new SendPaymentFeedback { OrderId = message.OrderId });
        }

        public void Timeout(PaymentFeedbackTimeout state)
        {
            s_log.Error("Payment feedback timeout");
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderState> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);
        }
    }
}
