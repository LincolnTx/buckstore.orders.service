using MediatR;
using buckstore.orders.service.application.DTOs;

namespace buckstore.orders.service.application.Queries
{
    public class HistoricalOrdersMonthlyReportQuery : IRequest<HistoricalMonthlyReportDto>
    {
        // o default deve ser zero
        public int StatusIdFilter { get; set; }
    }
}