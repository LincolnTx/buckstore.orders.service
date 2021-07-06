using System;
using System.Net.Http;
using GlobalPayments.Api.PaymentMethods;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.infrastructure.common.Proxy.Core;
using buckstore.orders.service.application.Adapters.MessageBroker;

namespace buckstore.orders.service.infrastructure.proxy.globalPayments.Adapters
{
    public class GlobalPaymentsService : WebApiBaseRequest, IPaymentService
    {
        public GlobalPaymentsService(HttpClient httpClient) : base(httpClient)
        {
        }
        
        public void CreditCardPay(CreditCarPaymentDto paymentDtoInformation)
        {
            var paymentCard = new CreditCardData
            {
                Number = paymentDtoInformation.CardNumber,
                ExpMonth = paymentDtoInformation.CardExpiration.Month,
                ExpYear = paymentDtoInformation.CardExpiration.Year,
                Cvn = paymentDtoInformation.CardSecurityNumber,
                CardHolderName = paymentDtoInformation.CardHolderName
            };

            try
            {
                var response = paymentCard.Charge(paymentDtoInformation.OrderAmount)
                    .WithCurrency("BRL")
                    .Execute();

                var result = response.ResponseCode;
                var message = response.ResponseMessage;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}