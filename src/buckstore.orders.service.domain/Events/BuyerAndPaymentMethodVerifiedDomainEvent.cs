using System;
using buckstore.orders.service.domain.Aggregates.BuyerAggregate;

namespace buckstore.orders.service.domain.Events
{
    public class BuyerAndPaymentMethodVerifiedDomainEvent : Event
    {
        public PaymentMethod Payment { get; set; }
        public Guid OrderId { get; set; }
        public bool IsNewPaymentMethod { get; set; } 

        public BuyerAndPaymentMethodVerifiedDomainEvent(PaymentMethod payment, Guid orderId, bool isNewPaymentMethod)
        {
            Payment = payment;
            OrderId = orderId;
            IsNewPaymentMethod = isNewPaymentMethod;
        }
    }
}