using System;
using System.Net.Http;
using buckstore.orders.service.application.Adapters.MessageBroker;
using buckstore.orders.service.infrastructure.common.Proxy.Core;

namespace buckstore.orders.service.infrastructure.proxy.globalPayments.Adapters
{
    public class GlobalPaymentsService : WebApiBaseRequest, IPaymentService
    {
        public GlobalPaymentsService(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}