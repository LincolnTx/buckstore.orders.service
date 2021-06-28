using System;
using System.Collections.Generic;
using System.Linq;
using buckstore.orders.service.domain.SeedWork;

namespace buckstore.orders.service.domain.Aggregates.BuyerAggregate
{
    public class Buyer : Entity, IAggregateRoot
    {
        public string Cpf { get; private set; }
        public string Name { get; private set; }
        private List<PaymentMethod> _paymentMethods;
        public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

        public Buyer(string cpf, string name)
        {
            Cpf = cpf;
            Name = name;
        }

        protected Buyer()
        {
            _paymentMethods = new List<PaymentMethod>();
        }

        public bool VerifyPaymentMethod(string cardNumber, DateTime expiration)
        {
            var existingPayment = _paymentMethods.SingleOrDefault(p => p.IsEqualTo(cardNumber, expiration));

            return existingPayment != null;
        }

        public void AddPaymentMethod(string alias, string cardNumber, string securityNumber, string cardHolderName,
            DateTime expiration)
        {
            var payment = new PaymentMethod(alias, cardNumber, securityNumber, cardHolderName, expiration);
            _paymentMethods.Add(payment);
        }
    }
}