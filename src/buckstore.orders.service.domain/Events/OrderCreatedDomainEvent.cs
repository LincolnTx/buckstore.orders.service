namespace buckstore.orders.service.domain.Events
{
    public class OrderCreatedDomainEvent : Event
    {
        public string Cpf { get; set; }

        public OrderCreatedDomainEvent(string cpf)
        {
            Cpf = cpf;
        }
    }
}