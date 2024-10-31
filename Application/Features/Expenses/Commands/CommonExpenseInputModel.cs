using Application.Services;
using Domain.Enum;
using Domain.Models;

namespace Application.Features.Expenses.Commands
{
    public class CommonExpenseInputModel
    {
        public DateTime Date { get; set; }

        public ExpenseType Type { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal Quantity { get; set; }

        public int? ArticleId { get; set; }

        public IEnumerable<int> SelectedArableLands { get; set; } = Enumerable.Empty<int>();
    }
}
