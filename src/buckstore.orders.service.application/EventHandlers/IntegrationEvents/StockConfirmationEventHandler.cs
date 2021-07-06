using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;
using buckstore.orders.service.domain.Exceptions;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class StockConfirmationEventHandler : EventHandler<StockConfirmationIntegrationEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMediator _bus;

        public StockConfirmationEventHandler(IOrderRepository orderRepository, IUnitOfWork uow, IMediator bus)
        {
            _orderRepository = orderRepository;
            _uow = uow;
            _bus = bus;
        }

        public override async Task Handle(StockConfirmationIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindById(notification.OrderId);

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), $"Ordem informada não existente {notification.OrderId}, para stock success");
            }
            
            order.ChangeStatus(OrderStatus.Pending);
            if (!await _uow.Commit())
            {
                await _bus.Publish(new ExceptionNotification("002", $"Erro ao mudar status da ordem para {OrderStatus.Pending.Name}"),
                    cancellationToken);
                await Task.FromCanceled(cancellationToken);
            }

            // enviar evento OrderPaymentPendingIntegrationEvent
            // verificar como pegar o método de pagamento de uma ordem
        }
    }
}