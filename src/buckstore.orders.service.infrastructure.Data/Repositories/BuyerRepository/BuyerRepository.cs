using System.Threading.Tasks;
using buckstore.orders.service.domain.Aggregates.BuyerAggregate;
using buckstore.orders.service.infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace buckstore.orders.service.infrastructure.Data.Repositories.BuyerRepository
{
    public class BuyerRepository : Repository<Buyer>, IBuyerRepository
    {
        public BuyerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<Buyer> GetBuyerByCpf(string cpf)
        {
            return await _dbSet.FirstOrDefaultAsync(buyer => buyer.Cpf == cpf);
        }
    }
}