using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.application.EventHandlers;
using buckstore.orders.service.application.Adapters.MessageBroker;

namespace buckstore.orders.service.application.IntegrationEvents
{
    public class StockUpdateIntegrationEventHandler : EventHandler<StockUpdateIntegrationEvent>
    {
        private readonly IMessageProducer<StockUpdateIntegrationEvent> _producer;
        private readonly ILogger<StockUpdateIntegrationEventHandler> _logger;

        public StockUpdateIntegrationEventHandler(IMessageProducer<StockUpdateIntegrationEvent> producer, 
            ILogger<StockUpdateIntegrationEventHandler> logger)
        {
            _producer = producer;
            _logger = logger;
        }

        public override async Task Handle(StockUpdateIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _producer.Produce(notification);
            _logger.LogInformation($"Atualização de estoque enviada para a manger deviado a execução da ordem {notification.OrderId}");
        }
    }
}