using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;
using MediatR;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class PaymentRefusedEventHandler : EventHandler<PaymentRefusedIntegrationEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        private readonly IMediator _bus;

        public PaymentRefusedEventHandler(IOrderRepository orderRepository, IUnitOfWork uow, IMapper mapper, IMediator bus)
        {
            _orderRepository = orderRepository;
            _uow = uow;
            _mapper = mapper;
            _bus = bus;
        }

        public override async Task Handle(PaymentRefusedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindById(notification.OrderId);
            order.ChangeStatus(OrderStatus.Cancelled);

            if (!await _uow.Commit())
                return;

            var orderItems = _mapper.Map<IEnumerable<OrderItemDto>>(order.OrderItems);
            var @event = new OrderRollbackIntegrationEvent(orderItems, order.Id);

            await _bus.Publish(@event, cancellationToken);

            // talvez enviar email e talvez adicionar o reject reason na ordem
        }
    }
}