using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using buckstore.orders.service.application.DTOs;

namespace buckstore.orders.service.application.Queries
{
    public class ListOrdersQuery : IRequest<GetOrdersResponseDto>
    {
        private const int PageSize = 10;
        public Guid BuyerId { get; set; }
        public List<string> StatusFilter { get; set; }
        public int Page { get; set; }

        public ListOrdersQuery(Guid buyerId, List<string> statusFilter, int page)
        {
            BuyerId = buyerId;
            StatusFilter = ValidateArrays(statusFilter);
            Page = page > 0 ? (page - 1 ) * PageSize : 0;
        }
        
        List<string> ValidateArrays(List<string> values)
        {
            if (values.Any() && values[0].Contains(','))
            {
                return values[0].Split(',').ToList();
            }

            return values;
        }
    }
}