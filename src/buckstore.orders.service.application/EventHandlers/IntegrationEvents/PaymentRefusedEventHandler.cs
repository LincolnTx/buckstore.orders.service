using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class PaymentRefusedEventHandler : EventHandler<PaymentRefusedIntegrationEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;

        public PaymentRefusedEventHandler(IOrderRepository orderRepository, IUnitOfWork uow)
        {
            _orderRepository = orderRepository;
            _uow = uow;
        }

        public override async Task Handle(PaymentRefusedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindById(notification.OrderId);
            order.ChangeStatus(OrderStatus.Cancelled);

            await _uow.Commit();
            
            // talvez enviar email e talvez adicionar o reject reason na ordem
        }
    }
}