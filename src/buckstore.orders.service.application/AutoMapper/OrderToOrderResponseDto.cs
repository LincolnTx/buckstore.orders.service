using System;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;

namespace buckstore.orders.service.application.AutoMapper
{
    public class OrderToOrderResponseDto : MappingProfile
    {

        public OrderToOrderResponseDto()
        {
            CreateMap<Order, OrderResponseDto>()
                .ConvertUsing(src => new OrderResponseDto(
                    src.Id,
                    src.OrderStatusId,
                    Enumeration.FromValue<OrderStatus>(src.OrderStatusId).Name,
                    src.Value,
                    src.OrderDate
                ));
        }
        
    }
}