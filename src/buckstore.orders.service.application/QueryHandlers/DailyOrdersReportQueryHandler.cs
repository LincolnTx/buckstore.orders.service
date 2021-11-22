using Dapper;
using System;
using MediatR;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.application.Queries;
using buckstore.orders.service.application.Queries.ViewModels;

namespace buckstore.orders.service.application.QueryHandlers
{
    public class DailyOrdersReportQueryHandler : QueryHandler, IRequestHandler<DailyOrdersReportQuery, DailyOrdersReportDto>
    {
        private readonly IMediator _bus;

        public DailyOrdersReportQueryHandler(IMediator bus)
        {
            _bus = bus;
        }

        public async Task<DailyOrdersReportDto> Handle(DailyOrdersReportQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                foreach (var error in request.GetValidationResult().Errors)
                {
                    await _bus.Publish(new ExceptionNotification("019", error.ErrorMessage, error.PropertyName),
                        cancellationToken);
                }

                return default;
            }
            using var dbConnection = DbConnection;
            
            var sqlCommand = BuildSqlQuery(request.StatusIdFilter);
            
            try
            {
                var data = await dbConnection.QueryAsync<DailyOrderReportViewModel>(sqlCommand, new
                {
                    statusId = request.StatusIdFilter,
                    startDate = request.StartDate,
                    endDate = request.EndDate
                });

                return new DailyOrdersReportDto(data);
            }
            catch (Exception e)
            {
                await _bus.Publish(new ExceptionNotification("020", 
                        "Erro ao gerar relatório diário de ordens"),
                    cancellationToken);
                return default;
            }
        }

        private string BuildSqlQuery(int statusFilter)
        {
            var queryString = new StringBuilder();

            queryString.Append("SELECT to_char(date_trunc('day', o.\"OrderDate\"), 'DD') AS Day,");
            queryString.Append("to_char(date_trunc('day', o.\"OrderDate\"), 'Mon') AS Month, ");
            queryString.Append("to_char(date_trunc('day', o.\"OrderDate\"), 'MM') AS MonthNumber, ");
            queryString.Append(" to_char(date_trunc('day', o.\"OrderDate\"), 'YYYY') as Year, ");
            queryString.Append("sum(o.value) AS DailySum  FROM \"order\" o WHERE ");

            if (statusFilter != 0)
            {
                queryString.Append("o.\"OrderStatusId\" = @statusId AND ");
            }

            queryString.Append("o.\"OrderDate\" >= @startDate AND o.\"OrderDate\" <= @endDate ");
            queryString.Append("GROUP BY date_trunc('day', o.\"OrderDate\") ORDER BY date_trunc('day', o.\"OrderDate\")");

            return queryString.ToString();
        }
    }
}