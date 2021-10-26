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
}