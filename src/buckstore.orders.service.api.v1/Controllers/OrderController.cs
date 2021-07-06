using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.application.Commands;

namespace buckstore.orders.service.api.v1.Controllers
{
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
            // pegar id do usuario do token
            var response = await _bus.Send(newOrder);

            return Response(201, response);
        }
    }
}