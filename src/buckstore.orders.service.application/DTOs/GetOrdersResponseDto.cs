using System;
using System.Collections.Generic;

namespace buckstore.orders.service.application.DTOs
{
    public class GetOrdersResponseDto
    {
        public IEnumerable<OrderResponseDto> Orders { get; set; }

        public GetOrdersResponseDto(IEnumerable<OrderResponseDto> orders)
        {
            Orders = orders;
        }
    }

    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatus { get; set; }
        public decimal OrderAmount { get; set; }
        public DateTime OrderDate { get; set; }
        // talvez adicionar endereço para cada ordem
    }
}