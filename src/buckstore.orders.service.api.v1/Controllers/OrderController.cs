using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.application.Commands;
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
        public async Task<IActionResult> PostOrder([FromBody] NewOrderCommand newOrder)
        {
            //var userId = GetTokenClaim("id");
            newOrder.UserId = Guid.Parse("56b3360d-cd70-4296-8805-91079cba6bf8");
            
            var response = await _bus.Send(newOrder);

            return Response(201, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var response = await _bus.Send(new GetOrderQuery{ OrderId = orderId});
            
            return Response(201, response);
        }
    }
}