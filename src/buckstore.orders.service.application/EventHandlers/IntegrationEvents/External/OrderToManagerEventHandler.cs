using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.IntegrationEvents.External;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents.External
{
    public class OrderToManagerEventHandler : EventHandler<OrderToManagerIntegrationEvent>
    {
        public override Task Handle(OrderToManagerIntegrationEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}