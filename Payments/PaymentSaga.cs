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
        , IHandleMessages<ShipmentCreated>
        , IHandleTimeouts<PaymentFeedbackTimeout>
    {
        private static readonly Logger s_log = LogManager.GetCurrentClassLogger();

        public void Handle(OrderPlaced message)
        {
            Data.OrderId = message.OrderId;

            RequestTimeout<PaymentFeedbackTimeout>(TimeSpan.FromSeconds(5));

            Bus.Send(new SendPaymentFeedback { OrderId = message.OrderId });
            Bus.Send(new CreateShipment { OrderId = message.OrderId });
        }

        public void Handle(PaymentFeedbackSent message)
        {
            s_log.Info($"Payment feedback was sent for order {message.OrderId}");
            Data.PaymentFeedbackStatus = MessageStatus.Success;
            TryMarkAsComplete();
        }

        public void Handle(ShipmentCreated message)
        {
            s_log.Info($"Payment feedback was sent for order {message.OrderId}");
            Data.ShipmentStatus = MessageStatus.Success;
            TryMarkAsComplete();
        }

        private void TryMarkAsComplete()
        {
            if (Data.PaymentFeedbackStatus != MessageStatus.NotStarted &&
                Data.ShipmentStatus != MessageStatus.NotStarted)
            {
                MarkAsComplete();
            }
        }

        public void Timeout(PaymentFeedbackTimeout state)
        {
            s_log.Error("Payment feedback timeout");

        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderState> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);

            mapper.ConfigureMapping<PaymentFeedbackSent>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);
            mapper.ConfigureMapping<ShipmentCreated>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);
        }
    }
}
