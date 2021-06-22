using buckstore.orders.service.domain.SeedWork;

namespace buckstore.orders.service.domain.Aggregates.PaymentAggregate
{
    public class PaymentStatus : Enumeration
    {
        public static PaymentStatus Pending = new PaymentStatus(1, nameof(Pending));
        public static PaymentStatus Accepted = new PaymentStatus(2, nameof(Accepted));
        public static PaymentStatus Cancelled = new PaymentStatus(3, nameof(Cancelled));
        public PaymentStatus(int id, string name) : base(id, name)
        {
        }
    }
}