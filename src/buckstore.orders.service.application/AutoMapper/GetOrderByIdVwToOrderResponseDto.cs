using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.application.Queries.ViewModels;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;

namespace buckstore.orders.service.application.AutoMapper
{
    public class GetOrderByIdVwToOrderResponseDto : MappingProfile
    {
        public GetOrderByIdVwToOrderResponseDto()
        {
            CreateOrderResponse();
        }

        void CreateOrderResponse()
        {
            CreateMap<GetOrderByIdViewModel, OrderResponseDto>()
                .ConvertUsing(src => new OrderResponseDto
                {
                    Id = src.Id,
                    OrderAmount = src.value,
                    OrderDate = src.OrderDate,
                    OrderStatusId = src.OrderStatusId,
                    OrderStatus = Enumeration.FromValue<OrderStatus>(src.OrderStatusId).Name
                });
        }
    }
}