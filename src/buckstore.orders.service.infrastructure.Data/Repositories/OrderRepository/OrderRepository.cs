using buckstore.orders.service.infrastructure.Data.Context;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;

namespace buckstore.orders.service.infrastructure.Data.Repositories.OrderRepository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}