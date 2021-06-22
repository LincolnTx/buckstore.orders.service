using System;

namespace buckstore.orders.service.application.IntegrationEvents
{
    public class ProductUpdatedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public ProductUpdatedIntegrationEvent(Guid id, string productName, int quantity, decimal price) : base(DateTime.Now)
        {
            Id = id;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }
    }
}