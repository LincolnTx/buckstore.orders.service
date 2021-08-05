using buckstore.orders.service.application.DTOs;

namespace buckstore.orders.service.application.Adapters.MessageBroker
{
    public interface IPaymentService
    {
        bool CreditCardPay(CreditCarPaymentDto paymentDtoInformation);
    }
}