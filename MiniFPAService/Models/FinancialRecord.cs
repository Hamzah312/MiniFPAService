namespace MiniFPAService.Models
{
    public class FinancialRecord
    {
        public int Id { get; set; }
        public string Type { get; set; } // e.g. Budget, Actuals
        public string Account { get; set; }
        public string Department { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
    }
}

