using MediatR;
using System.Linq;
using buckstore.orders.service.application.DTOs;

namespace buckstore.orders.service.application.Queries
{
    public class ListOrdersQuery : IRequest<GetOrdersResponseDto>
    {
        private const int PageSize = 10;
        public string BuyerId { get; set; }
        public int[] StatusFilter { get; set; }
        public int Page { get; set; }

        public ListOrdersQuery(string buyerId, string[] statusFilter, int page)
        {
            BuyerId = buyerId;
            StatusFilter = ValidateArrays(statusFilter);
            Page = page > 0 ? (page - 1 ) * PageSize : 0;
        }
        
        int[] ValidateArrays(string[] values)
        {
            if (values.Any() && values[0].Contains(','))
            {
                var separate = values[0].Split(',');
                return separate.Select(int.Parse).ToArray();
            }

            return values.Select(int.Parse).ToArray();
        }
    }
}