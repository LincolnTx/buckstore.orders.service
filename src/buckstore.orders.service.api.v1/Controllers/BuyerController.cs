using System;
using MediatR;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.application.Queries;
using buckstore.orders.service.api.v1.ResponseDtos;

namespace buckstore.orders.service.api.v1.Controllers
{
    [Authorize]
    public class BuyerController : BaseController
    {
        private readonly IMediator _bus;
        
        public BuyerController(INotificationHandler<ExceptionNotification> notifications, IMediator bus) : base(notifications)
        {
            _bus = bus;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListPaymentMethodsDto), (int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> BuyerPaymentMethods()
        {
            var buyerId = GetTokenClaim("id");
            var listPaymentQuery = new ListPaymentMethodsByBuyerQuery(Guid.Parse(buyerId));

            var response = await _bus.Send(listPaymentQuery);

            return Response(Ok(new BaseResponseDto<ListPaymentMethodsDto>(true, response)));
        }
    }
}