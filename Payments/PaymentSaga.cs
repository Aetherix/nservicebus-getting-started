using NServiceBus.Saga;
using Shared;
using NLog;
using System;
using NServiceBus;

namespace Payments
{
    public class PaymentSaga : Saga<OrderState>
        , IAmStartedByMessages<OrderPlaced>
        , IHandleMessages<PaymentFeedbackSent>
        , IHandleTimeouts<PaymentFeedbackTimeout>
    {
        private static readonly Logger s_log = LogManager.GetCurrentClassLogger();

        public void Handle(OrderPlaced message)
        {
            Data.OrderId = message.OrderId;

            RequestTimeout<PaymentFeedbackTimeout>(TimeSpan.FromSeconds(5));

            Bus.Send(new SendPaymentFeedback { OrderId = message.OrderId });
        }

        public void Handle(PaymentFeedbackSent message)
        {
            s_log.Info($"Payment feedback was sent for order {message.OrderId}");

            MarkAsComplete();
        }

        public void Timeout(PaymentFeedbackTimeout state)
        {
            s_log.Error("Payment feedback timeout");

            MarkAsComplete();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderState> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);

            mapper.ConfigureMapping<PaymentFeedbackSent>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);
        }
    }
}
