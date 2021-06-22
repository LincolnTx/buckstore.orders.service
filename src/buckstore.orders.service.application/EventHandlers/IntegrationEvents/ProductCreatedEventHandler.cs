using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.IntegrationEvents;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class ProductCreatedEventHandler : EventHandler<ProductCreatedIntegrationEvent>
    {
        public override Task Handle(ProductCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}