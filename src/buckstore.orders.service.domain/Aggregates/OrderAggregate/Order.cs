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
        public IEnumerable<OrderItem> OrderItems => _orderItems.AsReadOnly();
        private decimal _value;
        public decimal Value => _value;
        private DateTime _orderDate;
        private Guid _paymentMethodId;
        public Guid PaymentMethodId => _paymentMethodId;

        public Order(Guid buyerId, string userName, string cpf,  Address address,
            string alias, string cardNumber, string securityNumber, DateTime cardExpiration, string cardHolderName)
        {
            _orderItems = new List<OrderItem>();
            _buyerid = buyerId;
            _orderStatusId = OrderStatus.StockConfirmation.Id;
            _orderDate = DateTime.Now;
            Address = address;
            SetId();
            _paymentMethodId = Guid.Empty;
            AddDomainEvent(new OrderCreatedDomainEvent(cpf, cardNumber, cardExpiration, alias, securityNumber, 
                cardHolderName, Id));
        }

        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public void AddDeliveryItem(Guid productId, string productName, int quantity, decimal price)
        {
            if (quantity < 1)
            {
                throw new Exception("Produto deve ter pelo menos 1 unidade");
            }

            var orderItem = new OrderItem(productId, productName, quantity, price);
            _orderItems.Add(orderItem);
            CalculateGoods(price * quantity);
        }

        public void SetPaymentId(Guid id)
        {
            _paymentMethodId = id;
        }

        public void ChangeStatus(OrderStatus status)
        {
            OrderStatus = status;
            _orderStatusId = status.Id;
        }
        // TODO Criar domain event para quando o pedido mudar de status, Cancelado ou aceito
        void CalculateGoods(decimal price)
        {
            _value += price;
        }
    }
}