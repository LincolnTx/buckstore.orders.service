using System;

namespace buckstore.orders.service.application.DTOs
{
    public class CreditCarPaymentDto
    {
        public string CardNumber { get; set; }
        public DateTime CardExpiration { get; set; }
        public string CardSecurityNumber { get; set; }
        public string CardHolderName { get; set; }
        public decimal OrderAmount { get; set; }
        
    }
}