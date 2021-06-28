using System;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.domain.SeedWork;

namespace buckstore.orders.service.domain.Aggregates.BuyerAggregate
{
    public class PaymentMethod :Entity
    {
        private string _cardNumber;
        private string _alias;
        private string _securityNumber;
        private string _cardHolderName;
        private DateTime _expiration;
        
        protected PaymentMethod() { }

        public PaymentMethod(string alias, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration)
        {
            _cardNumber = !string.IsNullOrWhiteSpace(cardNumber) ? cardNumber : throw new OrderingDomainException(nameof(cardNumber));;
            _securityNumber = !string.IsNullOrWhiteSpace(securityNumber) ? securityNumber : throw new OrderingDomainException(nameof(securityNumber));
            _cardHolderName = !string.IsNullOrWhiteSpace(cardHolderName) ? cardHolderName : throw new OrderingDomainException(nameof(cardHolderName));

            if (expiration < DateTime.Now)
            {
                throw new OrderingDomainException(nameof(expiration));
            }
            _expiration = expiration;
            _alias = alias;
        }
        
        public bool IsEqualTo(string cardNumber, DateTime expiration)
        {
            return _cardNumber == cardNumber
                   && _expiration == expiration;
        }
    }
}