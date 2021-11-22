using System.Collections.Generic;
using buckstore.orders.service.application.Queries.ViewModels;

namespace buckstore.orders.service.application.DTOs
{
    public class DailyFinishedOrderReportDto
    {
        public IEnumerable<DailyFinishedOrderReportViewModel> ReportData { get; set; }

        public DailyFinishedOrderReportDto(IEnumerable<DailyFinishedOrderReportViewModel> reportData)
        {
            ReportData = reportData;
        }
    }
}