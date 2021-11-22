using System;
using MediatR;
using FluentValidation.Results;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.application.Validations;

namespace buckstore.orders.service.application.Queries
{
    public class DailyFinishedOrderReportQuery : IRequest<DailyFinishedOrderReportDto>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MinValue { get; set; }

        private ValidationResult _validationResult;

        public DailyFinishedOrderReportQuery(DateTime startDate, DateTime endDate, decimal minValue)
        {
            StartDate = startDate;
            EndDate = endDate.AddDays(1);
            MinValue = minValue;
        }

        public bool IsValid()
        {
            _validationResult = new DailyFinishedOrderReportQueryValidations().Validate(this);

            return _validationResult.IsValid;
        }

        public ValidationResult GetValidationResult()
        {
            return _validationResult;
        }
    }
}