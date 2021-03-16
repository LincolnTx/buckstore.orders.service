using System;
using System.Collections.Generic;
using buckstore.orders.service.domain.Events;
using buckstore.orders.service.domain.SeedWork;

namespace buckstore.orders.service.domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public Address Address { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems { get; private set; }
        private int _orderStatusId;
        private double _value;
        public double Value => _value;

        public Order(Address address, string cpf)
        {
            Address = address;
            _orderStatusId = OrderStatus.Pending.Id;
            _orderItems = new List<OrderItem>();
            AddDomainEvent(new OrderCreatedDomainEvent(cpf));
        }

        protected Order()
        {
        }

        public void AddDeliveryItem(string productName, int quantity, double price)
        {
            if (quantity < 1)
            {
                throw new Exception("Produto deve ter pelo menos 1 unidade");
            }

            var orderItem = new OrderItem(productName, quantity, price);
            _orderItems.Add(orderItem);
        }

        void CalculateGoods(double price)
        {
            _value += price;
        }
    }
}