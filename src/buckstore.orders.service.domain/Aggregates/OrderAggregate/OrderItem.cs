using System;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.domain.Exceptions;

namespace buckstore.orders.service.domain.Aggregates.OrderAggregate
{
    public class OrderItem : Entity
    {
        private string _productName;
        public string ProductName => _productName;
        private int _quantity;
        private decimal _price;

        public OrderItem(Guid itemId, string productName, int quantity, decimal price)
        {
            if (quantity <= 0)
            {
                throw new OrderingDomainException("Quantidade inválida");
            }
            SetId(itemId);
            _productName = productName;
            _quantity = quantity;
            _price = price;
        }

        protected OrderItem()
        {
        }

        public void AddUnits(int units)
        {
            
        }
    }
}