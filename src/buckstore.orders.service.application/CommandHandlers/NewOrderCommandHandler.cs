using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.application.Commands;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;
using buckstore.orders.service.application.IntegrationEvents.External;

namespace buckstore.orders.service.application.CommandHandlers
{
    public class NewOrderCommandHandler : CommandHandler, IRequestHandler<NewOrderCommand, bool>
    {
        private readonly ILogger<NewOrderCommandHandler> _logger;
        public NewOrderCommandHandler(IUnitOfWork uow, 
            IMediator bus, 
            INotificationHandler<ExceptionNotification> notifications,
            ILogger<NewOrderCommandHandler> logger) 
            : base(uow, bus, notifications)
        {
            _logger = logger;
        }

        public async Task<bool> Handle(NewOrderCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var address = new Address(request.Street, request.ZipCode, request.District, request.City, request.State);
            var order = new Order(request.UserId, request.UserName, request.Cpf, address, request.PaymentMethodId,
                request.CardAlias, request.CardNumber, request.CardSecurityNumber, request.CardExpiration, request.CardHolderName );

            foreach (var item in request.OrderItems)
            {
                order.AddDeliveryItem(item.ProductId, item.ProductName, item.Quantity, item.Price);
            }

            if (!await Commit())
            {
                await _bus.Publish(new ExceptionNotification("001", "Erro ao adicionar uma nova ordem"),
                    cancellationToken);
                return false;
            }
            
            _logger.LogInformation($"Nova ordem inserida ao banco {order.Id}");

            var orderCreatedEvent = new OrderCreatedIntegrationEvent(request.OrderItems, order.Id);
            await _bus.Publish(orderCreatedEvent, cancellationToken);
            
            return true;
        }
    }
}