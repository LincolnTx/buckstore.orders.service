using buckstore.orders.service.domain.SeedWork;

namespace buckstore.orders.service.domain.Aggregates.BuyerAggregate
{
    public class Buyer : Entity, IAggregateRoot
    {
        public string Cpf { get; private set; }

        public Buyer(string cpf)
        {
            Cpf = cpf;
        }

        protected Buyer()
        {
        }
    }
}