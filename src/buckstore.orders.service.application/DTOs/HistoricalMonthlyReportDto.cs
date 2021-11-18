using System.Collections.Generic;
using buckstore.orders.service.application.Queries.ViewModels;

namespace buckstore.orders.service.application.DTOs
{
    public class HistoricalMonthlyReportDto
    {
        public IEnumerable<HistoricalOrderMonthlyViewModel> ReportData { get; set; }

        public HistoricalMonthlyReportDto(IEnumerable<HistoricalOrderMonthlyViewModel> reportData)
        {
            ReportData = reportData;
        }
    }
}