using System;
using FluentValidation;
using CreditCardValidator;
using FluentValidation.Results;
using buckstore.orders.service.application.Commands;

namespace buckstore.orders.service.application.Validations
{
    public class NewOrderValidations : AbstractValidator<NewOrderCommand>
    {
        public NewOrderValidations()
        {
            ValidateUserName();
            ValidateAddress();
            ValidateCardNumber();
            ValidateCardExpiration();
        }

        protected void ValidateUserName()
        {
            RuleFor(orders => orders.UserName)
                .MaximumLength(100)
                .NotEmpty()
                .WithMessage("O nome deve ser informado").WithErrorCode("001");
        }
        
        protected void ValidateAddress()
        {
            RuleFor(orders => orders)
                .Custom((order, context) =>
                {
                    if (string.IsNullOrEmpty(order.City)
                        || string.IsNullOrEmpty(order.Street)
                        || string.IsNullOrEmpty(order.ZipCode)
                        || string.IsNullOrEmpty(order.District))
                    {
                        var failure = new ValidationFailure(nameof(order.City), "O endereçao informado é inválido")
                        {
                            ErrorCode = "002"
                        };

                        context.AddFailure(failure);
                    }
                });
        }
        
        protected void ValidateCardNumber()
        {
            RuleFor(order => order)
                .Custom((order, context) =>
                {
                    if (order.CardNumber == string.Empty) return;
                    
                    var detector = new CreditCardDetector(order.CardNumber);
                    if (!detector.IsValid())
                    {
                        var failure = new ValidationFailure(nameof(order.CardNumber), "Número do cartão inválido")
                        {
                            ErrorCode = "003"
                        };
                         context.AddFailure(failure);
                        
                    }
                });

            RuleFor(order => order.CardNumber)
                .MaximumLength(22)
                .NotEmpty()
                .When(cmd => cmd.PaymentMethodId == default)
                .WithMessage("O número do cartão é obrigatório e deve conter ate 22 dígitos")
                .WithErrorCode("004");

        }
        
        protected void ValidateCardExpiration()
        {
            RuleFor(order => order.CardExpiration)
                .GreaterThan(DateTime.Now)
                .NotEmpty()
                .WithMessage("A data de vencimento do cartão deve ser uma data válida")
                .WithErrorCode("005");
        }
    }
    
}