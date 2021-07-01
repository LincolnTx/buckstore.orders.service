using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.application.Adapters.MessageBroker;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class OrderCreatedEventHandler : EventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IMessageProducer<OrderCreatedIntegrationEvent> _messageProducer;
        private readonly IMediator _bus;
        private readonly ILogger<OrderCreatedEventHandler> _logger;

        public OrderCreatedEventHandler(IMessageProducer<OrderCreatedIntegrationEvent> messageProducer, 
            ILogger<OrderCreatedEventHandler> logger, IMediator bus)
        {
            _messageProducer = messageProducer;
            _logger = logger;
            _bus = bus;
        }

        public override async Task Handle(OrderCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _messageProducer.Produce(notification);
            
            _logger.LogInformation($"Pedido de confirmação de estoque enviado para a ordem {notification.OrderId}");
        }
    }
}