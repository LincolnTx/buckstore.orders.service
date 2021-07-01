using System;

namespace buckstore.orders.service.application.IntegrationEvents
{
    public class StockConfimationFailIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public bool Success { get; set; }

        public StockConfimationFailIntegrationEvent(Guid orderId, bool success) : base(DateTime.Now)
        {
            OrderId = orderId;
            Success = success;
        }
    }
}