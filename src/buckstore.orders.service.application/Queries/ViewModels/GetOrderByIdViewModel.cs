using System;

namespace buckstore.orders.service.application.Queries.ViewModels
{
    public class GetOrderByIdViewModel
    {
        public Guid Id { get; set; }
        public int OrderStatusId { get; set; }
        public decimal value { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderItems { get; set; }
    }
}