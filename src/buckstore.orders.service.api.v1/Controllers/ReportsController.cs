using System;
using System.Net;
using System.Threading.Tasks;
using buckstore.orders.service.api.v1.ResponseDtos;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.application.Queries;
using buckstore.orders.service.domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace buckstore.orders.service.api.v1.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly IMediator _bus;
        public ReportsController(INotificationHandler<ExceptionNotification> notifications, IMediator bus) : base(notifications)
        {
            _bus = bus;
        }
        
        [HttpGet("historical/{statusIdFilter}")]
        [ProducesResponseType(typeof(HistoricalMonthlyReportDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetHistoricalReport(int statusIdFilter = 0)
        {
            var request = new HistoricalOrdersMonthlyReportQuery { StatusIdFilter = statusIdFilter};
            var response = await _bus.Send(request);

            return Response(Ok(new BaseResponseDto<HistoricalMonthlyReportDto>(true, response)));
        }
        
        [HttpGet("daily/{statusIdFilter}/{startDate}/{endDate}")]
        [ProducesResponseType(typeof(DailyOrdersReportDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> DailyReport(int statusIdFilter, DateTime startDate, DateTime endDate)
        {
            var request = new DailyOrdersReportQuery(startDate, endDate, statusIdFilter);
            var response = await _bus.Send(request);

            return Response(Ok(new BaseResponseDto<DailyOrdersReportDto>(true, response)));
        }
        
        [HttpGet("{minPrice}/{startDate}/{endDate}")]
        [ProducesResponseType(typeof(DailyFinishedOrderReportDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> MinPriceReport(decimal minPrice, DateTime startDate, DateTime endDate)
        {
            var request = new DailyFinishedOrderReportQuery(startDate, endDate, minPrice);
            var response = await _bus.Send(request);
        
            return Response(Ok(new BaseResponseDto<DailyFinishedOrderReportDto>(true, response)));
        }
    }
}