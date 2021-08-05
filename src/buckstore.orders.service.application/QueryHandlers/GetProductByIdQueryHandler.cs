using System;
using Dapper;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.application.Queries;
using buckstore.orders.service.application.Queries.ViewModels;

namespace buckstore.orders.service.application.QueryHandlers
{
    public class GetProductByIdQueryHandler : QueryHandler, IRequestHandler<GetOrderQuery, OrderResponseDto>
    {
        private readonly IMediator _bus;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IMediator bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        public async Task<OrderResponseDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            using (var dbConnection = DbConnection)
            {
                const string sqlCommand = "SELECT o.\"Id\", o.\"OrderStatusId\", o.value, o.\"OrderDate\" " + 
                                          "FROM orders.order o WHERE o.\"Id\" = @orderId";

                try
                {
                    var data = await dbConnection.QueryFirstAsync<GetOrderByIdViewModel>(sqlCommand, new
                    {
                        orderId = request.OrderId
                    });
                    
                    return _mapper.Map<OrderResponseDto>(data);
                }
                catch (Exception e)
                {
                    await _bus.Publish(new ExceptionNotification("002",
                        "Ordem não encontrada. É possível que o código da ordem seja inválido", 
                        "orderCode"), CancellationToken.None);
                    
                    return null;
                }
            }
        }
    }
}