using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.IntegrationEvents;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class ProductCreatedEventHandler : EventHandler<ProductCreatedIntegrationEvent>
    {
        public async override Task Handle(ProductCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            
        }
    }
}