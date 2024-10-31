using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Reports
{
    public class FinancialSummaryReportModel : IDynamicDataGridModel
    {
        public int Id { get; set; }

        public string WorkingSeason { get; set; } = string.Empty;

        public decimal SumIncome { get; set; }

        public decimal SumExpense { get; set; }

        public decimal Profit { get; set; }

        public List<FinancialSummaryReportIncomes> IncomesByArableLand { get; set; } = new();

        public List<FinancialSummaryReportExpenses> ExpensesByArableLand { get; set; } = new();

        public decimal ExpensesForMachinery { get; set; }
    }

    public class FinancialSummaryReportIncomes
    {
        public string ArableLandName { get; set; } = string.Empty;

        public decimal Harvest { get; set; }

        public decimal Subsidies { get; set; }
    }

    public class FinancialSummaryReportExpenses
    {
        public string ArableLandName { get; set; } = string.Empty;
        public decimal Fuel { get; set; }
        public decimal Pesticides { get; set; }
        public decimal Fertilizers { get; set; }
        public decimal Seeds { get; set; }
        public decimal Rent { get; set; }
        public decimal Harvest { get; set; }
    }
}
