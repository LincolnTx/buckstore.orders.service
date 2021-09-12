using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.infrastructure.common.Proxy.Core;
using buckstore.orders.service.application.Adapters.Proxy.Payment;
using buckstore.orders.service.infrastructure.environment.Configuration;
using buckstore.orders.service.infrastructure.proxy.globalPayments.Adapters.Dtos.Request;

namespace buckstore.orders.service.infrastructure.proxy.globalPayments.Adapters
{
    public class FkdPaymentsService : WebApiBaseRequest, IPaymentService
    {
        private readonly ILogger<FkdPaymentsService> _logger;
        private readonly FkdPayConfiguration _paymentServiceConfiguration;
        public FkdPaymentsService(HttpClient httpClient, 
            ILogger<FkdPaymentsService> logger, 
            FkdPayConfiguration paymentServiceConfiguration) 
            : base(httpClient)
        {
            _logger = logger;
            _paymentServiceConfiguration = paymentServiceConfiguration;
        }
        
        public async Task<bool> RegisterNewCard(RegisterCreditCarPaymentDto paymentDtoInformation)
        {
            var requestData = new RegisterPaymentCardRequest
            {
                CardNumber = paymentDtoInformation.CardNumber,
                ExpMonth = paymentDtoInformation.CardExpiration.Month,
                ExpYear = paymentDtoInformation.CardExpiration.Year,
                Cvv = paymentDtoInformation.CardSecurityNumber,
                CardHolderName = paymentDtoInformation.CardHolderName
            };

            try
            {
                await PostApiAsync<RegisterPaymentCardRequest>($"{_paymentServiceConfiguration.BaseUrl}payment-card",
                    requestData);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro ao registrar o cartão do usuário {e.Message} {e.InnerException}");

                return false;
            }
        }
        public async Task<bool> CreditCardPay(MakePurchaseDto paymentInfo)
        {
            var requestData = new MakePaymentRequest
            {
                CardNumber = paymentInfo.CardNumber,
                PurchaseValue = paymentInfo.PurchaseValue
            };

            try
            {
                await PostApiAsync<MakePaymentRequest>($"{_paymentServiceConfiguration.BaseUrl}purchase", requestData);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro ao relizar pagamanto da compra {e.Message} {e.InnerException}");

                return false;
            }
        }
    }
}