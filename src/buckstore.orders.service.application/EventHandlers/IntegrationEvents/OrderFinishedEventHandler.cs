using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class OrderFinishedEventHandler : EventHandler<OrderFinishedIntegrationEvent>
    {
        private readonly IUnitOfWork _uow;
        private readonly IOrderRepository _orderRepository;

        public OrderFinishedEventHandler(IUnitOfWork uow, IOrderRepository orderRepository)
        {
            _uow = uow;
            _orderRepository = orderRepository;
        }

        public override async Task Handle(OrderFinishedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindById(notification.OrderId);
            
            order.ChangeStatus(OrderStatus.Accept);

            await _uow.Commit();
            // talvez enviar email de aviso
        }
    }
}