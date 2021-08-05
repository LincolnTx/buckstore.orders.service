using System;

namespace buckstore.orders.service.application.IntegrationEvents
{
    public class OrderFinishedIntegrationEvent : IntegrationEvent
    {
        // possivelmente vai mudar
        public Guid OrderId { get; set; }

        public OrderFinishedIntegrationEvent(Guid orderId) : base(DateTime.Now)
        {
            OrderId = orderId;
        }
    }
}