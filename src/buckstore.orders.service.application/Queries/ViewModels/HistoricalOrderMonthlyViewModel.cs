namespace buckstore.orders.service.application.Queries.ViewModels
{
    public class HistoricalOrderMonthlyViewModel
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string MonthNumber { get; set; }
        public decimal MonthlySum { get; set; }

    }
}