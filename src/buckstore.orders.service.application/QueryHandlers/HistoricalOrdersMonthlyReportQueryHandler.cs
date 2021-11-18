using System;
using System.Text;
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
        public async Task<HistoricalMonthlyReportDto> Handle(HistoricalOrdersMonthlyReportQuery request, CancellationToken cancellationToken)
        {
            using var dbConnection = DbConnection;

            var sqlCommand = QueryBuilder(request.StatusIdFilter);
           
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

        private string QueryBuilder(int statusFilter)
        {
            var query = new StringBuilder();

            query.Append("SELECT to_char(date_trunc('month', o.\"OrderDate\"), 'YYYY') AS Year, ");
            query.Append("to_char(date_trunc('month', o.\"OrderDate\"), 'Mon') AS Month, ");
            query.Append("to_char(date_trunc('month', o.\"OrderDate\"), 'MM') AS MonthNumber, ");
            query.Append("sum(o.value) AS MonthlySum FROM \"order\" o ");

            if (statusFilter != 0)
            {
                query.Append("WHERE o.\"OrderStatusId\" = @statusId ");
            }

            query.Append("GROUP BY date_trunc('month', o.\"OrderDate\") ");
            query.Append("ORDER BY date_trunc('month', o.\"OrderDate\")");

            return query.ToString();
        }
    }
}