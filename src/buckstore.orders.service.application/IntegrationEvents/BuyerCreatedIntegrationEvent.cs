using System;

namespace buckstore.orders.service.application.IntegrationEvents
{
    public class BuyerCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        
        public BuyerCreatedIntegrationEvent(Guid id, string name, string cpf) : base(DateTime.Now)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
        }
    }
}