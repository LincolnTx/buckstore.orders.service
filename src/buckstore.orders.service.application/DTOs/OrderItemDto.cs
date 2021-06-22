using System;

namespace buckstore.orders.service.application.DTOs
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public int QuantitySold { get; set; }
    }
}