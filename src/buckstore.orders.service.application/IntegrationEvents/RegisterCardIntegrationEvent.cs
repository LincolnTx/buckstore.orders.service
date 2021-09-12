using System;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.domain.Events;

namespace buckstore.orders.service.application.IntegrationEvents
{
    public class RegisterCardIntegrationEvent : IntegrationEvent
    {
        public RegisterCreditCarPaymentDto PaymentCredCard { get; set; }
        
        public RegisterCardIntegrationEvent(RegisterCreditCarPaymentDto paymentCredCard) : base(DateTime.Now)
        {
            PaymentCredCard = paymentCredCard;
        }
    }
}