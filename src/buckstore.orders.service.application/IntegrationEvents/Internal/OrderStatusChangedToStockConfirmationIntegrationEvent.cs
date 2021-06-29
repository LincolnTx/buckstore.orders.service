using System;

namespace buckstore.orders.service.application.IntegrationEvents.Internal
{
    public class OrderStatusChangedToStockConfirmationIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }

        public OrderStatusChangedToStockConfirmationIntegrationEvent(Guid orderId) : base(DateTime.Now)
        {
            OrderId = orderId;
        }
    }
}