using System;
using MediatR;
using System.Collections.Generic;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.application.Validations;

namespace buckstore.orders.service.application.Commands
{
    public class NewOrderCommand : Command, IRequest<bool>
    {
        private readonly List<OrderItemDto> _orderItems;
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Cpf { get; set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string CardNumber { get; set; }
        public string CardAlias { get; set; }
        public string CardHolderName  { get; set; }
        public DateTime CardExpiration  { get; set; }
        public string CardSecurityNumber { get; set; }
        public IEnumerable<OrderItemDto> OrderItems => _orderItems;
        public Guid PaymentMethodId { get; set; }

        public NewOrderCommand()
        {
            _orderItems = new List<OrderItemDto>();
        }
        
        public override bool IsValid()
        {
            ValidationResult = new NewOrderValidations().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}