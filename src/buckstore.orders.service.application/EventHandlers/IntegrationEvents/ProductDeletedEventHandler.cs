﻿using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.IntegrationEvents;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class ProductDeletedEventHandler : EventHandler<ProductDeletedIntegrationEvent>
    {
        public override Task Handle(ProductDeletedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}