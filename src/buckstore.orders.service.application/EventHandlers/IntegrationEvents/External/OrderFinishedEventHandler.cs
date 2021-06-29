using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.IntegrationEvents.External;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents.External
{
    public class OrderFinishedEventHandler : EventHandler<OrderFinishedIntegrationEvent>
    {
        // talvez vai ser deletado
        public override Task Handle(OrderFinishedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}