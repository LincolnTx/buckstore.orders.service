using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.IntegrationEvents.External;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents.External
{
    public class StockConfirmationEventHandler : EventHandler<StockConfirmationIntegrationEvent>
    {
        public async override Task Handle(StockConfirmationIntegrationEvent notification, CancellationToken cancellationToken)
        {
            // TODO atualizar o status da ordem e mandar um evento de confirmar pagamaento, simular um espeara
        }
    }
}