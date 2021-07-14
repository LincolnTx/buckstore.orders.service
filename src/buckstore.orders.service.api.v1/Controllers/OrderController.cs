using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.application.Commands;

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
            var userId = GetTokenClaim("id");
            newOrder.UserId = Guid.Parse(userId);
            
            var response = await _bus.Send(newOrder);

            return Response(201, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            // mudar para usar query
            var response = await _bus.Send(new GetOrderCommand{ OrderId =  orderId});
            
            return Response(201, response);
        }
    }
}