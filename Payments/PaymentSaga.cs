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
        , IHandleTimeouts<CreateShipmentTimeout>
    {
        private static readonly Logger s_log = LogManager.GetCurrentClassLogger();

        public void Handle(OrderPlaced message)
        {
            Data.OrderId = message.OrderId;

            RequestTimeout<PaymentFeedbackTimeout>(TimeSpan.FromSeconds(5));
            Bus.Send(new SendPaymentFeedback { OrderId = message.OrderId });

            RequestTimeout<CreateShipmentTimeout>(TimeSpan.FromSeconds(5));
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
            s_log.Info($"Shipment created for order {message.OrderId}");
            Data.ShipmentStatus = MessageStatus.Success;
            TryMarkAsComplete();
        }

        public void Timeout(PaymentFeedbackTimeout state)
        {
            s_log.Error("Payment feedback timeout");
            Data.PaymentFeedbackStatus = MessageStatus.Fail;
            TryMarkAsComplete();
        }

        public void Timeout(CreateShipmentTimeout state)
        {
            s_log.Error("Shipment timed out");
            Data.ShipmentStatus = MessageStatus.Fail;
            TryMarkAsComplete();
        }

        private void TryMarkAsComplete()
        {
            s_log.Info($"Trying to mark saga as complete");

            if (Data.PaymentFeedbackStatus != MessageStatus.NotStarted &&
                Data.ShipmentStatus != MessageStatus.NotStarted)
            {
                MarkAsComplete();
            }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderState> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);

            mapper.ConfigureMapping<PaymentFeedbackSent>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);
            mapper.ConfigureMapping<ShipmentCreated>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);
        }
    }
}
