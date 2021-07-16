using System;
using MediatR;
using System.Collections.Generic;
using buckstore.orders.service.application.DTOs;

namespace buckstore.orders.service.application.Queries
{
    public class GetAllOrdersQuery : IRequest<GetOrdersResponseDto>
    {
        public Guid UserId { get; set; }
        public List<int> OrderStatus { get; set; }

        
    }
}