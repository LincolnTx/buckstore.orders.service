using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.application.Commands;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;

namespace buckstore.orders.service.application.CommandHandlers
{
    public class NewOrderCommandHandler : CommandHandler, IRequestHandler<NewOrderCommand, OrderResponseDto>
    {
        private readonly ILogger<NewOrderCommandHandler> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public NewOrderCommandHandler(IUnitOfWork uow, 
            IMediator bus, 
            INotificationHandler<ExceptionNotification> notifications,
            ILogger<NewOrderCommandHandler> logger, IOrderRepository orderRepository, IMapper mapper) 
            : base(uow, bus, notifications)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponseDto> Handle(NewOrderCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return default;
            }

            var address = new Address(request.Street, request.ZipCode, request.District, request.City, request.State, request.AddressNumber);
            var order = new Order(request.UserId, request.UserName, request.Cpf, address,
                request.CardAlias, request.CardNumber, request.CardSecurityNumber, request.CardExpiration, request.CardHolderName, request.PaymentMethodId);

            foreach (var item in request.OrderItems)
            {
                order.AddDeliveryItem(item.ProductId, item.ProductName, item.Quantity, item.Price);
            }

            if (request.DiscountPercent != default)
            {
                order.AddDiscount(request.DiscountPercent);
            }
            _orderRepository.Add(order);

            if (await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken))
            {
                await _bus.Publish(new ExceptionNotification("001", "Erro ao adicionar uma nova ordem"),
                    cancellationToken);
                return default;
            }
            
            _logger.LogInformation($"Nova ordem inserida ao banco {order.Id}");

            var orderCreatedEvent = new OrderCreatedIntegrationEvent(request.OrderItems, order.Id);
            await _bus.Publish(orderCreatedEvent, cancellationToken);

            return _mapper.Map<OrderResponseDto>(order);
        }
    }
}