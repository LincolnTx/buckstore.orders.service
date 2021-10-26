using System;
using System.Net;
using MediatR;
using System.Threading.Tasks;
using buckstore.orders.service.api.v1.ResponseDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.application.Commands;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.application.Queries;

namespace buckstore.orders.service.api.v1.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IMediator _bus;
        public OrderController(INotificationHandler<ExceptionNotification> notifications, IMediator bus) : base(notifications)
        {
            _bus = bus;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> PostOrder([FromBody] NewOrderCommand newOrder)
        {
            var userId = GetTokenClaim("id");

           var name = GetTokenClaim("userName");
            newOrder.UserId = Guid.Parse(userId);
            newOrder.UserName = name;
            
            var response = await _bus.Send(newOrder);

            return Response(Ok(new BaseResponseDto<OrderResponseDto>(true, response)));
        }

        [HttpGet]
        [ProducesResponseType(typeof(OrderResponseDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var response = await _bus.Send(new GetOrderQuery{ OrderId = orderId});
            
            return Response(Ok(new BaseResponseDto<OrderResponseDto>(true, response)));
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(GetOrdersResponseDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> ListOrders(string[] statusFilter, int pageNumber)
        { 
            var userId = GetTokenClaim("id");
            var query = new ListOrdersQuery(Guid.Parse(userId), statusFilter, pageNumber);

            var response = await _bus.Send(query);

            return Response(Ok(new BaseResponseDto<GetOrdersResponseDto>(true, response)));
        }
    }
}