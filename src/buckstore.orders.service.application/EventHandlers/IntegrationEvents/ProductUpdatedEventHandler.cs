using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.IntegrationEvents;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class ProductUpdatedEventHandler : EventHandler<ProductUpdatedIntegrationEvent>
    {
        public override Task Handle(ProductUpdatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}