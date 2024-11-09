namespace WebUI.ServicesModel.Expenses
{
    public class DetailsExpenseModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Type { get; set; }

        public decimal Sum { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal Quantity { get; set; }
        public int? ArticleId { get; set; }

        public int WorkingSeasonId { get; set; }

        public IEnumerable<int> SelectedArableLands { get; set; } = Enumerable.Empty<int>();
    }
}
