using System;
using System.Threading.Tasks;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.domain.Aggregates.BuyerAggregate;

namespace buckstore.orders.service.domain.Aggregates.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> FindById(Guid id);
        Task<Order> FindOrderWithItemsById(Guid id);
        Task<PaymentMethod> FindPaymentMethod(Guid orderId);
    }
}