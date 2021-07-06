using buckstore.orders.service.application.DTOs;

namespace buckstore.orders.service.application.Adapters.MessageBroker
{
    public interface IPaymentService
    {
        void CreditCardPay(CreditCarPaymentDto paymentDtoInformation);
    }
}