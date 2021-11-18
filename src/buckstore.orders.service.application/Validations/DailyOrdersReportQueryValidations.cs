using System;
using FluentValidation;
using buckstore.orders.service.application.Queries;
using FluentValidation.Results;

namespace buckstore.orders.service.application.Validations
{
    public class DailyOrdersReportQueryValidations : AbstractValidator<DailyOrdersReportQuery>
    {
        public DailyOrdersReportQueryValidations()
        {
            ValidateDates();
        }

        private void ValidateDates()
        {
            RuleFor(query => query.StartDate)
                .NotEmpty()
                .Must(DateValidation)
                .LessThanOrEqualTo(query => query.EndDate)
                .When(query => DateValidation(query.EndDate))
                .WithMessage(
                    "A data de inicio é obrigatória e deve ser uma data válida, com valor menor do que a data de fim");
            
            RuleFor(query => query.EndDate)
                .NotEmpty()
                .Must(DateValidation)
                .GreaterThanOrEqualTo(query => query.StartDate)
                .When(query => DateValidation(query.StartDate))
                .WithMessage(
                    "A data de fim é obrigatória e deve ser uma data válida, com valor maior do que a data de incio");

            RuleFor(query => query)
                .Custom((query, context) =>
                {
                    if (!DateValidation(query.StartDate) || !DateValidation(query.EndDate))
                        return;

                    if (!ValidationTimeSpan(query.StartDate, query.EndDate))
                    {
                        var failure = new ValidationFailure(nameof(query.EndDate), "O intervalo entre as datas não pode ser maior do que seis meses")
                        {
                            ErrorCode = "018"
                        };
                        context.AddFailure(failure);
                    }
                });

        }

        private bool DateValidation(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        private bool ValidationTimeSpan(DateTime startDate, DateTime endDate)
        {
            var monthsDifference = (startDate.Subtract(endDate).Days / 30) * -1;
            return monthsDifference <= 6;
        }
    }
}