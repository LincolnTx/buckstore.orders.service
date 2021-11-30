using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.application.Adapters.Proxy.Payment;
using buckstore.orders.service.domain.Exceptions;


namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class RegisterCardIntegrationEventHandler : EventHandler<RegisterCardIntegrationEvent>
    {
        private IPaymentService _paymentService;
        private IMediator _bus;

        public RegisterCardIntegrationEventHandler(IPaymentService paymentService, IMediator bus)
        {
            _paymentService = paymentService;
            _bus = bus;
        }

        public override async Task Handle(RegisterCardIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var result = await _paymentService.RegisterNewCard(notification.PaymentCredCard);

            if (!result)
            {
                await _bus.Publish(new ExceptionNotification("012", "Erro ao registrar cartão do usuário"),
                    cancellationToken);
            }
        }
    }
}