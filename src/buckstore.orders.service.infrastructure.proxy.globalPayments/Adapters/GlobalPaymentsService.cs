using System.Net.Http;
using GlobalPayments.Api.Entities;
using Microsoft.Extensions.Logging;
using GlobalPayments.Api.PaymentMethods;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.infrastructure.common.Proxy.Core;
using buckstore.orders.service.application.Adapters.MessageBroker;

namespace buckstore.orders.service.infrastructure.proxy.globalPayments.Adapters
{
    public class GlobalPaymentsService : WebApiBaseRequest, IPaymentService
    {
        private readonly ILogger<GlobalPaymentsService> _logger;
        public GlobalPaymentsService(HttpClient httpClient, ILogger<GlobalPaymentsService> logger) 
            : base(httpClient)
        {
            _logger = logger;
        }
        
        public bool CreditCardPay(CreditCarPaymentDto paymentDtoInformation)
        {
            bool validTransaction = false;
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

               validTransaction =  result == "00";

            }
            catch (BuilderException  e)
            {
                _logger.LogError("Erro ao buildar método de pagamento {0}", e.Message);
            }
            catch (UnsupportedTransactionException e)
            {
                _logger.LogError("Erro de transção com a API de pagamentos {0}", e.Message);
            }

            return validTransaction;
        }
    }
}