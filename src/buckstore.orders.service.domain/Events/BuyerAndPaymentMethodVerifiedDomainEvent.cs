using System;
using buckstore.orders.service.domain.Aggregates.BuyerAggregate;

namespace buckstore.orders.service.domain.Events
{
    public class BuyerAndPaymentMethodVerifiedDomainEvent : Event
    {
        public PaymentMethod Payment { get; set; }
        public Guid OrderId { get; set; }

        public BuyerAndPaymentMethodVerifiedDomainEvent(PaymentMethod payment, Guid orderId)
        {
            Payment = payment;
            OrderId = orderId;
        }
    }
}