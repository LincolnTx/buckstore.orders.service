using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class PaymentRefusedEventHandler : EventHandler<PaymentRefusedIntegrationEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;

        public PaymentRefusedEventHandler(IOrderRepository orderRepository, IUnitOfWork uow, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public override async Task Handle(PaymentRefusedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindById(notification.OrderId);
            order.ChangeStatus(OrderStatus.Cancelled);

            if (!await _uow.Commit())
                return;
            
            // enviar evento de rollback para a api de products

            var orderItems = _mapper.Map<IEnumerable<OrderItemDto>>(order.OrderItems);
            var @event = new OrderRollbackIntegrationEvent(orderItems, order.Id);

            // talvez enviar email e talvez adicionar o reject reason na ordem
        }
    }
}