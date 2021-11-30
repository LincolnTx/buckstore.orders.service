using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.Adapters.MessageBroker;
using buckstore.orders.service.application.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class OrderRollbackIntegrationEventHandler : EventHandler<OrderRollbackIntegrationEvent>
    {
        private readonly IMessageProducer<OrderRollbackIntegrationEvent> _messageProducer;
        private readonly ILogger<OrderRollbackIntegrationEventHandler> _logger;

        public OrderRollbackIntegrationEventHandler(IMessageProducer<OrderRollbackIntegrationEvent> messageProducer, ILogger<OrderRollbackIntegrationEventHandler> logger)
        {
            _messageProducer = messageProducer;
            _logger = logger;
        }

        public override async Task Handle(OrderRollbackIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _messageProducer.Produce(notification);
            
            _logger.LogInformation($"Mensagem de rollback enviada para a api de prodtos, ordem = {notification.OrderId}");
        }
    }
}