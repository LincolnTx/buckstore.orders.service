using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class OrderFinishedEventHandler : EventHandler<OrderFinishedIntegrationEvent>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _bus;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrderFinishedEventHandler(IUnitOfWork uow, IOrderRepository orderRepository, 
            IMediator bus, IMapper mapper)
        {
            _uow = uow;
            _orderRepository = orderRepository;
            _bus = bus;
            _mapper = mapper;
        }

        public override async Task Handle(OrderFinishedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindById(notification.OrderId);
            
            order.ChangeStatus(OrderStatus.Accept);

            if (await _uow.Commit())
            {
                var orderItems = _mapper.Map<IEnumerable<OrderItemDto>>(order.OrderItems);
                await _bus.Publish(new StockUpdateIntegrationEvent(orderItems, notification.OrderId), cancellationToken);
            }
            // talvez enviar email de aviso
        }
    }
}