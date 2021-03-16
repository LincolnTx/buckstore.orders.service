using buckstore.orders.service.domain.SeedWork;

namespace buckstore.orders.service.domain.Aggregates.OrderAggregate
{
    public class OrderItem : Entity
    {
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public double Price { get; private set; }

        public OrderItem(string productName, int quantity, double price)
        {
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }

        protected OrderItem()
        {
        }
    }
}