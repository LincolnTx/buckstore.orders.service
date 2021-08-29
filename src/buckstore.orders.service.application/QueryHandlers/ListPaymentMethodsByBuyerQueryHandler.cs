using System;
using System.Linq;
using Dapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.application.Queries;

namespace buckstore.orders.service.application.QueryHandlers
{
    public class ListPaymentMethodsByBuyerQueryHandler : QueryHandler, IRequestHandler<ListPaymentMethodsByBuyerQuery, ListPaymentMethodsDto>
    {
        private readonly IMediator _bus;

        public ListPaymentMethodsByBuyerQueryHandler(IMediator bus)
        {
            _bus = bus;
        }


        public async Task<ListPaymentMethodsDto> Handle(ListPaymentMethodsByBuyerQuery request, CancellationToken cancellationToken)
        {
            using var dbConnection = DbConnection;
            const string sqlCommand = "SELECT pm.\"Id\", pm.\"CardHolderName\", pm.\"CardNumber\", " +
                                      "pm.\"Expiration\", pm.\"Alias\" " + 
                                      "FROM orders.payment_methods pm WHERE pm.\"BuyerId\" =@buyerId";

            try
            {
                var data = await dbConnection.QueryAsync<PaymentMethodDto>(sqlCommand, new
                {
                    buyerId = request.BuyerId
                });
                
                data.ToList().ForEach(item => item.FixCardNumber());

                return new ListPaymentMethodsDto(data);
            }
            catch (Exception e)
            {
                await _bus.Publish(new ExceptionNotification("003",
                    "Usuário não encontrado no sistema",
                    "userId"),
                    cancellationToken);
                
                return default;
            }
        }
    }
}