using System;
using buckstore.orders.service.application.IntegrationEvents;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class OrderPaymentPendingIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardSecurityNumber { get; set; }
        public DateTime CardExpiration { get; set; }
        public decimal OrderAmount { get; set; }

        public OrderPaymentPendingIntegrationEvent(Guid orderId,
            string cardNumber,
            string cardHolderName,
            string cardSecurityNumber,
            DateTime cardExpiration,
            decimal orderAmount) 
            : base(DateTime.Now)
        {
            OrderId = orderId;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardSecurityNumber = cardSecurityNumber;
            CardExpiration = cardExpiration;
            OrderAmount = orderAmount;
        }
    }
}