using buckstore.orders.service.domain.SeedWork;

namespace buckstore.orders.service.domain.Aggregates.OrderAggregate
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        public Address(string street, string zipCode, string district, string city, string state)
        {
            Street = street;
            ZipCode = zipCode;
            District = district;
            City = city;
            State = state;
        }
    }
}