using System;
using System.Collections.Generic;
using buckstore.orders.service.domain.Events;
using buckstore.orders.service.domain.SeedWork;

namespace buckstore.orders.service.domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public Address Address { get; private set; }
        private Guid _buyerid;
        public Guid BuyerId => _buyerid;
        public OrderStatus OrderStatus { get; private set; }
        private int _orderStatusId;
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems { get; private set; }
        private decimal _value;
        public decimal Value => _value;
        private DateTime _orderDate;
        private Guid _paymentMethodId;

        public Order(Guid userId, string userName, string cpf,  Address address, int cardTypeId, string cardNumber, 
            string cardSecurityNumber, DateTime cardExpiration, Guid buyerId, Guid paymentMethodId)
        {
            _buyerid = buyerId;
            _paymentMethodId = paymentMethodId;
            _orderStatusId = OrderStatus.Submitted.Id;
            _orderDate = DateTime.Now;
            Address = address;
            AddDomainEvent(new OrderCreatedDomainEvent(cpf));
        }

        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public void AddDeliveryItem(Guid itemId, string productName, int quantity, decimal price)
        {
            if (quantity < 1)
            {
                throw new Exception("Produto deve ter pelo menos 1 unidade");
            }

            var orderItem = new OrderItem(itemId, productName, quantity, price);
            _orderItems.Add(orderItem);
        }
        // TODO Criar domain event para quando o pedido mudar de status, Cancelado ou aceito
        void CalculateGoods(decimal price)
        {
            _value += price;
        }
    }
}