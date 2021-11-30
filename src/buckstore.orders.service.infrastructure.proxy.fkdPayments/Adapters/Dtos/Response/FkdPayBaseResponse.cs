namespace buckstore.orders.service.infrastructure.proxy.globalPayments.Adapters.Dtos.Response
{
    public class FkdPayBaseResponse<TResponse> where TResponse :class
    {
        public bool Success { get; set; }
        public TResponse Data { get; set; }
    }
}