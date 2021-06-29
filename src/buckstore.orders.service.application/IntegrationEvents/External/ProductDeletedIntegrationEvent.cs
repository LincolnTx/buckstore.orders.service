using System;

namespace buckstore.orders.service.application.IntegrationEvents.External
{
    public class ProductDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }

        public ProductDeletedIntegrationEvent(Guid id) : base(DateTime.Now)
        {
            Id = id;
        }
    }
}