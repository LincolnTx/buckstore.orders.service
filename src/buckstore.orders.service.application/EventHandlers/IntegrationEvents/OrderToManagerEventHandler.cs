using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.IntegrationEvents;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class OrderToManagerEventHandler : EventHandler<OrderToManagerIntegrationEvent>
    {
        // talvez vai ser deletado
        public override Task Handle(OrderToManagerIntegrationEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}