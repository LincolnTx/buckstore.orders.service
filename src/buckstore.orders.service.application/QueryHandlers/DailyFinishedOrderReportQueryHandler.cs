using Dapper;
using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.application.Queries;
using buckstore.orders.service.application.Queries.ViewModels;

namespace buckstore.orders.service.application.QueryHandlers
{
    public class DailyFinishedOrderReportQueryHandler : QueryHandler, IRequestHandler<DailyFinishedOrderReportQuery, DailyFinishedOrderReportDto>
    {
        private readonly IMediator _bus;

        public DailyFinishedOrderReportQueryHandler(IMediator bus)
        {
            _bus = bus;
        }

        public async Task<DailyFinishedOrderReportDto> Handle(DailyFinishedOrderReportQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                foreach (var error in request.GetValidationResult().Errors)
                {
                    await _bus.Publish(new ExceptionNotification("021", error.ErrorMessage),
                        cancellationToken);
                }

                return default;
            }

            using var dbConnection = DbConnection;
            
            const string sqlCommand = "SELECT to_char(date_trunc('day', o.\"OrderDate\"), 'DD-MM') AS DayMonth, " +
                                      "count(o.value) TotalOrders, sum(o.value) AS TotalValue, " +
                                      "to_char(date_trunc('day', o.\"OrderDate\"), 'DD-Mon') as DateSpell " +
                                      "FROM \"order\" o WHERE o.value > @minValue AND o.\"OrderStatusId\" = 3 " +
                                      "AND o.\"OrderDate\" >= @startDate AND o.\"OrderDate\" <= @endDate " +
                                      "GROUP BY date_trunc('day', o.\"OrderDate\")";

            try
            {
                var data = await dbConnection.QueryAsync<DailyFinishedOrderReportViewModel>(sqlCommand, new
                {
                    minValue = request.MinValue,
                    startDate = request.StartDate,
                    endDate = request.EndDate
                });

                return new DailyFinishedOrderReportDto(data);
            }
            catch (Exception e)
            {
                await _bus.Publish(new ExceptionNotification("022", 
                        "Erro ao gerer relatório diário com preço mínimo"),
                    cancellationToken);
                
                return default;
            }

        }
    }
}