using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.domain.Events;
using buckstore.orders.service.domain.Aggregates.BuyerAggregate;

namespace buckstore.orders.service.application.EventHandlers.DomainEvents.OrderCreated
{
    public class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
    {
        private readonly IBuyerRepository _buyerRepository;

        public OrderCreatedDomainEventHandler(IBuyerRepository buyerRepository)
        {
            _buyerRepository = buyerRepository;
        }

        public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var buyer = await _buyerRepository.GetBuyerByCpf(notification.Cpf);

            if (!buyer.VerifyPaymentMethod(notification.CardNumber, notification.CardExpiration))
            {
                buyer.AddPaymentMethod(notification.Alias, notification.CardNumber, notification.SecurityNumber, 
                    notification.CardHolderName, notification.CardExpiration);
            }
        }
    }
}