using System.Collections.Generic;
using buckstore.orders.service.application.Queries.ViewModels;

namespace buckstore.orders.service.application.DTOs
{
    public class DailyOrdersReportDto
    {
        public IEnumerable<DailyOrderReportViewModel> ReportData { get; set; }

        public DailyOrdersReportDto(IEnumerable<DailyOrderReportViewModel> reportData)
        {
            ReportData = reportData;
        }
    }
}