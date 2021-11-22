namespace buckstore.orders.service.application.Queries.ViewModels
{
    public class DailyFinishedOrderReportViewModel
    {
        public string DayMonth { get; set; }
        public string TotalOrders { get; set; }
        public decimal TotalValue { get; set; }
        public string DateSpell { get; set; }
    }
}