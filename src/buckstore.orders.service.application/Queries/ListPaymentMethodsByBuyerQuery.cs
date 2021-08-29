using System;
using MediatR;
using buckstore.orders.service.application.DTOs;

namespace buckstore.orders.service.application.Queries
{
    public class ListPaymentMethodsByBuyerQuery : IRequest<ListPaymentMethodsDto>
    {
        public Guid BuyerId { get; set; }
     
        public ListPaymentMethodsByBuyerQuery(Guid buyerId)
        {
            BuyerId = buyerId;
        }
    }
}