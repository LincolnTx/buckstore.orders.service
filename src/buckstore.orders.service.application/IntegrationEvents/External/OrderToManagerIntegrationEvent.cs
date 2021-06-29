using System;
using System.Collections.Generic;
using buckstore.orders.service.application.DTOs;

namespace buckstore.orders.service.application.IntegrationEvents.External
{
    public class OrderToManagerIntegrationEvent : IntegrationEvent
    {
        public IEnumerable<OrderItemDto> Products {get;set;}

        public OrderToManagerIntegrationEvent(IEnumerable<OrderItemDto> products) : base(DateTime.Now)
        {
            Products = products;
        }
    }
}