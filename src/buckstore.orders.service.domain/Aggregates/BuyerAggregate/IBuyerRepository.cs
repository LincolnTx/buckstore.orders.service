using System;
using System.Threading.Tasks;
using buckstore.orders.service.domain.SeedWork;

namespace buckstore.orders.service.domain.Aggregates.BuyerAggregate
{
    public interface IBuyerRepository : IRepository<Buyer>
    {
        Task<Buyer> GetBuyerByCpf(string cpf);
        Task<Buyer> GetBuyerById(Guid id);
    }
}