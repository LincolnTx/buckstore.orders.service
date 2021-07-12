using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.domain.Events;
using buckstore.orders.service.domain.Aggregates.BuyerAggregate;
using buckstore.orders.service.domain.SeedWork;

namespace buckstore.orders.service.application.EventHandlers.DomainEvents.OrderCreated
{
    public class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
    {
        private readonly IBuyerRepository _buyerRepository;
        private readonly IUnitOfWork _uow;

        public OrderCreatedDomainEventHandler(IBuyerRepository buyerRepository, IUnitOfWork uow)
        {
            _buyerRepository = buyerRepository;
            _uow = uow;
        }

        public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        
        {
            var buyer = await _buyerRepository.GetBuyerByCpf(notification.Cpf);

           
            buyer.VerifyAndAddPaymentMethod(notification.Alias, notification.CardNumber, notification.SecurityNumber, 
                notification.CardHolderName, notification.CardExpiration, notification.OrderId);

            await _buyerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}