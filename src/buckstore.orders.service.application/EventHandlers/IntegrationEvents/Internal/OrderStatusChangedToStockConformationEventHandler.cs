using System;
using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;
using buckstore.orders.service.application.IntegrationEvents.Internal;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents.Internal
{
    public class OrderStatusChangedToStockConformationEventHandler : EventHandler<OrderStatusChangedToStockConfirmationIntegrationEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;

        public OrderStatusChangedToStockConformationEventHandler(IOrderRepository orderRepository, IUnitOfWork uow)
        {
            _orderRepository = orderRepository;
            _uow = uow;
        }

        public override async Task Handle(OrderStatusChangedToStockConfirmationIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindById(notification.OrderId);

            if (order == null)
            {
                throw new ArgumentNullException("OrderId", $"Orderm informada não existente {notification.OrderId} para StockConfirmation");
            }
            order.ChangeStatus(OrderStatus.StockConfirmation);

            await _uow.Commit();
        }
    }
}