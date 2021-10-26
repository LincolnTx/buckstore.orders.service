using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using buckstore.orders.service.infrastructure.Data.Context;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;
using buckstore.orders.service.domain.Aggregates.BuyerAggregate;

namespace buckstore.orders.service.infrastructure.Data.Repositories.OrderRepository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<Order> FindById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Order> FindOrderWithItemsById(Guid id)
        {
            return await _dbSet.Include(order => order.OrderItems)
                .FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task<PaymentMethod> FindPaymentMethod(Guid orderId)
        {
            var order = await _dbSet.FindAsync(orderId);
            return await _applicationDbContext.Set<PaymentMethod>().Where(p => p.Id == order.PaymentMethodId).FirstOrDefaultAsync();
        }
        
    }
}