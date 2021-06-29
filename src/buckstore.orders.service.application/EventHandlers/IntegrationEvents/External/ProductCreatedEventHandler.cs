using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.IntegrationEvents.External;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents.External
{
    public class ProductCreatedEventHandler : EventHandler<ProductCreatedIntegrationEvent>
    {
        public async override Task Handle(ProductCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            
        }
    }
}