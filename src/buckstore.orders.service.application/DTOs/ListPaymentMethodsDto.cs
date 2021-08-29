using System;
using System.Collections.Generic;

namespace buckstore.orders.service.application.DTOs
{
    public class ListPaymentMethodsDto
    {
        public IEnumerable<PaymentMethodDto> PaymentMethods { get; set; }
        
       
        public ListPaymentMethodsDto(IEnumerable<PaymentMethodDto> paymentMethods)
        {
            PaymentMethods = paymentMethods;
        }
    }

    public class PaymentMethodDto
    {
        public Guid Id { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public DateTime Expiration { get; set; }
        public string Alias { get; set; }

        public void FixCardNumber()
        {
            CardNumber = CardNumber.Substring(CardNumber.Length - 4);
        }
    }
}
