using buckstore.orders.service.domain.SeedWork;

namespace buckstore.orders.service.domain.Aggregates.OrderAggregate
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Pending = new OrderStatus(1, nameof(Pending));
        public static OrderStatus Accept = new OrderStatus(2, nameof(Accept));
        public static OrderStatus Cancelled = new OrderStatus(3, nameof(Cancelled));
        public OrderStatus(int id, string name) : base(id, name)
        {
        }
    }
}