using System;
using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;
using buckstore.orders.service.application.IntegrationEvents.External;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents.External
{
    public class StockConfirmationFailEventHandler : EventHandler<StockConfimationFailIntegrationEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;

        public StockConfirmationFailEventHandler(IOrderRepository orderRepository, IUnitOfWork uow)
        {
            _orderRepository = orderRepository;
            _uow = uow;
        }

        public override async Task Handle(StockConfimationFailIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindById(notification.OrderId);

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), $"Ordem não infromada não existente {notification.OrderId}, para stock fail");
            }
            
            order.ChangeStatus(OrderStatus.Cancelled);
            await _uow.Commit();
        }
    }
}