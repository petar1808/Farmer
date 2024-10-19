using Domain.Enum;

namespace Application.Features.Expenses.Commands.Create
{
    public class CreateExpenseInputModel
    {
        public DateTime Date { get; set; }

        public ExpenseType Type { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal Quantity { get; set; }

        public int? ArticleId { get; set; }

        public int WorkingSeasonId { get; set; }

        public IEnumerable<int> SelectedArableLands { get; set; } = Enumerable.Empty<int>();
    }
}
