using System;

namespace buckstore.orders.service.infrastructure.environment.Configuration
{
    public class KafkaConfiguration
    {
        public string ConnectionString { get;set; }
        public string Group { get;set; }
        public string OrdersToManager { get;set; }
        public string ProductsStockResponseSuccess { get; set; }
        public string ProductsStockResponseFail { get; set; }
        public string OrdersToProductsStockConfirmation { get;set; }
        public string AuthBuyerCreated { get; set; }
        public string OrderRollbackProducts { get; set; }
    }
}