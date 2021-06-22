namespace buckstore.orders.service.domain.Aggregates.PaymentAggregate
{
    public class Payment
    {
        private string _creditCard;
        public string CreditCardEnd => HandleCreditCardEnd();
        private int _paymentStatusId;
        public PaymentStatus PaymentStatus { get; }

        public Payment(string creditCard)
        {
            _creditCard = creditCard;
            _paymentStatusId = PaymentStatus.Pending.Id;
            PaymentStatus = PaymentStatus.Pending;
        }

        private string HandleCreditCardEnd()
        {
            return string.IsNullOrEmpty(_creditCard) ? string.Empty 
                : _creditCard.Substring(_creditCard.Length - 4);
        }
    }
}