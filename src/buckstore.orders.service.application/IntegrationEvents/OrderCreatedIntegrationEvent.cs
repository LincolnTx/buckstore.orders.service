using System;
using System.Collections.Generic;
using buckstore.orders.service.application.DTOs;

namespace buckstore.orders.service.application.IntegrationEvents
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public IEnumerable<OrderItemDto> Products {get;set;}
        public Guid OrderId { get; set; }
        
        public OrderCreatedIntegrationEvent(IEnumerable<OrderItemDto> products, Guid orderId) : base(DateTime.Now)
        {
            Products = products;
            OrderId = orderId;
        }
    }
}