using System;
using Dapper;
using MediatR;
using AutoMapper;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.application.Queries;
using buckstore.orders.service.application.Queries.ViewModels;

namespace buckstore.orders.service.application.QueryHandlers
{
    public class ListOrdersQueryHandler : QueryHandler, IRequestHandler<ListOrdersQuery, GetOrdersResponseDto>
    {
        private readonly IMapper _mapper;

        public ListOrdersQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<GetOrdersResponseDto> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        {
            using (var dbConnection = DbConnection)
            {
                var command = QueryBuilder(request.StatusFilter);

                try
                {
                    var data = await dbConnection.QueryAsync<GetOrderByIdViewModel>(command, new
                    {
                        userId = request.BuyerId,
                        statusFilter = request.StatusFilter,
                        pageNumber = request.Page,
                        pageSize = 10
                    });

                    var response = _mapper.Map<IEnumerable<OrderResponseDto>>(data);

                    return new GetOrdersResponseDto(response);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        string QueryBuilder(int[] statusFilter)
        {
            const string baseCommand = "SELECT o.\"Id\", o.\"OrderStatusId\", o.value, o.\"OrderDate\" FROM orders.order o"+ 
                                        " WHERE o.\"BuyerId\" = @userId";

            var queryBuilder = new StringBuilder(baseCommand);

            if (statusFilter != null && statusFilter.Any())
            {
                queryBuilder.Append(" AND o.\"OrderStatusId\"  = ANY(@statusFilter)");
            }

            queryBuilder.Append(
                " ORDER BY o.\"OrderDate\" DESC OFFSET @pageNumber ROWS FETCH NEXT @pageSize ROWS ONLY");

            return queryBuilder.ToString();
        }
    }
}