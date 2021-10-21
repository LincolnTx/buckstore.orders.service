using System;

namespace buckstore.orders.service.domain.Events
{
    public class OrderCreatedDomainEvent : Event
    {
        public string Cpf { get; set; }
        public string CardNumber { get; set; }
        public DateTime CardExpiration { get; set; }
        public string Alias { get; set; }
        public string SecurityNumber { get; set; }
        public string CardHolderName { get; set; }
        public Guid OrderId { get; set; }
        public Guid PaymentMethodId { get; set; }


        public OrderCreatedDomainEvent(string cpf, string cardNumber, DateTime cardExpiration, string @alias, string securityNumber, 
            string cardHolderName, Guid orderId, Guid paymentMethodId = default)
        {
            Cpf = cpf;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            Alias = alias;
            SecurityNumber = securityNumber;
            CardHolderName = cardHolderName;
            OrderId = orderId;
            PaymentMethodId = paymentMethodId;
        }
    }
}