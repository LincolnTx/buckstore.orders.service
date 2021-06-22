using System;

namespace buckstore.orders.service.infrastructure.environment.Configurations
{
    public class KafkaConfiguration
    {
        public string ConnectionString { get;set; }
        public string Group { get;set; }
        public string ManagerToOrdersCreate { get;set; }
        public string ManagerToOrdersUpdate { get;set; }
        public string ManagerToOrdersDelete { get;set; }
        public string OrdersToManager { get;set; }
        public string OrdersToProducts { get;set; }
    }
}