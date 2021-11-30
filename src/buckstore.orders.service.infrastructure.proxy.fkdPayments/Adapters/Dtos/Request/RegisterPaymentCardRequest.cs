namespace buckstore.orders.service.infrastructure.proxy.globalPayments.Adapters.Dtos.Request
{
    public class RegisterPaymentCardRequest
    {
        public string CardNumber { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string Cvv { get; set; }
        public string CardHolderName { get; set; }
    }
}