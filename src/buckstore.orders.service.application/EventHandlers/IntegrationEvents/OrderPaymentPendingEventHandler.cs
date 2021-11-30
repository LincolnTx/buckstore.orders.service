using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.application.Adapters.Proxy.Payment;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class OrderPaymentPendingEventHandler : EventHandler<OrderPaymentPendingIntegrationEvent>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMediator _bus;

        public OrderPaymentPendingEventHandler(IPaymentService paymentService, IMediator bus)
        {
            _paymentService = paymentService;
            _bus = bus;
        }

        public override async Task Handle(OrderPaymentPendingIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var result = await _paymentService.CreditCardPay(new MakePurchaseDto(notification.CardNumber,
                notification.OrderAmount));

            if (!result)
            {
                await _bus.Publish(new PaymentRefusedIntegrationEvent(notification.OrderId), cancellationToken);
                return;
            }

            await _bus.Publish(new OrderFinishedIntegrationEvent(notification.OrderId), cancellationToken);
        }
    }
}