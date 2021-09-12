namespace buckstore.orders.service.infrastructure.proxy.globalPayments.Adapters.Dtos.Response
{
    public class CheckCardBalanceResponse
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public decimal AvailableBalance { get; set; }
    }
}