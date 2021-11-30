using System.Threading.Tasks;
using buckstore.orders.service.application.DTOs;

namespace buckstore.orders.service.application.Adapters.Proxy.Payment
{
    public interface IPaymentService
    {
        Task<bool> RegisterNewCard(RegisterCreditCarPaymentDto paymentDtoInformation);
        Task<bool> CreditCardPay(MakePurchaseDto paymentInfo);
        // criar get 
    }
}