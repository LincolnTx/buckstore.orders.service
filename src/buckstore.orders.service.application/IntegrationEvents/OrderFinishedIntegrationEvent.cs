using System;
using System.Collections.Generic;
using buckstore.orders.service.application.DTOs;

namespace buckstore.orders.service.application.IntegrationEvents
{
    public class OrderFinishedIntegrationEvent : IntegrationEvent
    {
        public IEnumerable<OrderItemDto> Products {get;set;}

        public OrderFinishedIntegrationEvent(IEnumerable<OrderItemDto> products) : base(DateTime.Now)
        {
            Products = products;
        }
    }
}