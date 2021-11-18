using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.application.Queries;
using buckstore.orders.service.application.Queries.ViewModels;
using Dapper;

namespace buckstore.orders.service.application.QueryHandlers
{
    public class HistoricalOrdersMonthlyReportQueryHandler : QueryHandler, IRequestHandler<HistoricalOrdersMonthlyReportQuery, HistoricalMonthlyReportDto>
    {
        public async Task<HistoricalMonthlyReportDto
        > Handle(HistoricalOrdersMonthlyReportQuery request, CancellationToken cancellationToken)
        {
            using var dbConnection = DbConnection;

            const string sqlCommand = "SELECT to_char(date_trunc('month', o.\"OrderDate\"), 'YYYY') AS Year, " +
                                      "to_char(date_trunc('month', o.\"OrderDate\"), 'Mon') AS Month, " +
                                      "to_char(date_trunc('month', o.\"OrderDate\"), 'MM') AS MonthNumber, " +
                                      "sum(o.value) AS MonthlySum FROM \"order\" o " +
                                      "WHERE o.\"OrderStatusId\" = @statusId " +
                                      "GROUP BY date_trunc('month', o.\"OrderDate\") " +
                                      "ORDER BY date_trunc('month', o.\"OrderDate\")";
            try
            {
                var data = await dbConnection.QueryAsync<HistoricalOrderMonthlyViewModel>(sqlCommand, new
                {
                    statusId = request.StatusIdFilter
                });

                return new HistoricalMonthlyReportDto(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
                
        }
    }
}