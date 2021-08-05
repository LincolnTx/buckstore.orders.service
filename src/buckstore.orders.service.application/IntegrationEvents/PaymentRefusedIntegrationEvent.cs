using System;

namespace buckstore.orders.service.application.IntegrationEvents
{
    public class PaymentRefusedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public PaymentRefusedIntegrationEvent(Guid orderId) : base(DateTime.Now)
        {
            OrderId = orderId;
        }
    }
}