using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.domain.Events;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;

namespace buckstore.orders.service.application.EventHandlers.DomainEvents.OrderCreated
{
    public class BuyerAndPaymentMethodVerifiedEventHandler : EventHandler<BuyerAndPaymentMethodVerifiedDomainEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<BuyerAndPaymentMethodVerifiedEventHandler> _logger;

        public BuyerAndPaymentMethodVerifiedEventHandler(IOrderRepository orderRepository, 
            ILogger<BuyerAndPaymentMethodVerifiedEventHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public override async Task Handle(BuyerAndPaymentMethodVerifiedDomainEvent notification, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.FindById(notification.OrderId);

            orderToUpdate.SetPaymentId(notification.Payment.Id);
            
            _logger.LogInformation("Adicionando id do pagamento");
        }
    }
}