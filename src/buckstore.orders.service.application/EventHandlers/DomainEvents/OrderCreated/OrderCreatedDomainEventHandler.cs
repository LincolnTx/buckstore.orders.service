using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.domain.Events;
using MediatR;

namespace buckstore.orders.service.application.EventHandlers.DomainEvents.OrderCreated
{
    public class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
    {
        public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // vincular ordem ao cliente com o cpf recebido
            throw new System.NotImplementedException();
        }
    }
}