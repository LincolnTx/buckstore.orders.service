using System;
using buckstore.orders.service.application.DTOs;
using MediatR;

namespace buckstore.orders.service.application.Queries
{
    public class GetOrderQuery : IRequest<OrderResponseDto>
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}