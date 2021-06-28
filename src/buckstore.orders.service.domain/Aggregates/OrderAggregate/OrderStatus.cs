using buckstore.orders.service.domain.SeedWork;

namespace buckstore.orders.service.domain.Aggregates.OrderAggregate
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Submitted = new OrderStatus(1, nameof(Submitted));
        public static OrderStatus StockConfirmation = new OrderStatus(2, nameof(StockConfirmation));
        public static OrderStatus Pending = new OrderStatus(3, nameof(Pending));
        public static OrderStatus Accept = new OrderStatus(4, nameof(Accept));
        public static OrderStatus Cancelled = new OrderStatus(5, nameof(Cancelled));
        public OrderStatus(int id, string name) : base(id, name)
        {
        }
    }
}