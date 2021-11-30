using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;

namespace buckstore.orders.service.application.AutoMapper
{
    public class OrderItemToOrderItemDto : MappingProfile
    {
        public OrderItemToOrderItemDto()
        {
            CreateOrderItemDto();
        }

        private void CreateOrderItemDto()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ConvertUsing(src => new OrderItemDto
            {
                Quantity = src.Quantity,
                ProductId = src.ProducId,
                ProductName = src.ProductName
            });
        }
    }
}