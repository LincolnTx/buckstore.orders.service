namespace buckstore.orders.service.application.Queries.ViewModels
{
    public class DailyOrderReportViewModel
    {
        public string Day { get; set; }
        public string Month { get; set; }
        public string MonthNumber { get; set; }
        public string Year { get; set; }
        public decimal DailySum { get; set; }   
    }
}