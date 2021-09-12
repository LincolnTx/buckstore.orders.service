namespace buckstore.orders.service.infrastructure.proxy.globalPayments.Adapters.Dtos.Request
{
    public class MakePaymentRequest
    {
        public string CardNumber { get; set; }
        public decimal PurchaseValue { get; set; }
    }
}