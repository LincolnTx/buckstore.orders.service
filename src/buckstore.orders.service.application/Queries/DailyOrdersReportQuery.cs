using System;
using MediatR;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.application.Validations;
using FluentValidation.Results;

namespace buckstore.orders.service.application.Queries
{
    public class DailyOrdersReportQuery : IRequest<DailyOrdersReportDto>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusIdFilter { get; set; }

        private ValidationResult ValidationResult;

        public DailyOrdersReportQuery(DateTime startDate, DateTime endDate, int statusIdFilter)
        {
            StartDate = startDate;
            EndDate = endDate;
            StatusIdFilter = statusIdFilter;
        }

        public bool IsValid()
        {
            ValidationResult = new DailyOrdersReportQueryValidations().Validate(this);

            return ValidationResult.IsValid;
        }
        
        public ValidationResult GetValidationResult()
        {
            return ValidationResult;
        }
    }
}