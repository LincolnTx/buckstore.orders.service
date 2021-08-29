using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using buckstore.orders.service.infrastructure.Data.Context;
using buckstore.orders.service.domain.Aggregates.BuyerAggregate;

namespace buckstore.orders.service.infrastructure.Data.Repositories.BuyerRepository
{
    public class BuyerRepository : Repository<Buyer>, IBuyerRepository
    {
        public BuyerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<Buyer> GetBuyerByCpf(string cpf)
        {
            return await _dbSet.Include(b => b.PaymentMethods)
                .Where(b => b.Cpf == cpf).SingleOrDefaultAsync();
        }

        public async Task<Buyer> GetBuyerById(Guid id)
        {
            return await  _dbSet.Include(b => b.PaymentMethods)
                .Where(b => b.Id.Equals(id))
                .SingleOrDefaultAsync();
        }
    }
}