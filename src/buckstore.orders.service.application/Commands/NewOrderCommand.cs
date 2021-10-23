using System;
using MediatR;
using System.Collections.Generic;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.application.Validations;

namespace buckstore.orders.service.application.Commands
{
    public class NewOrderCommand : Command, IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Cpf { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int AddressNumber { get; set; }
        public string CardNumber { get; set; }
        public string CardAlias { get; set; }
        public string CardHolderName  { get; set; }
        public DateTime CardExpiration  { get; set; }
        public string CardSecurityNumber { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
        public Guid PaymentMethodId { get; set; }

        public NewOrderCommand()
        {
        }
        
        public override bool IsValid()
        {
            ValidationResult = new NewOrderValidations().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}