using Domain.Common;
using Domain.Enum;

namespace Domain.Models
{
    public class Expense : Entity<int>, ITenant
    {
        public Expense(
            DateTime date,
            ExpenseType type,
            decimal pricePerUnit,
            decimal quantity,
            int? articleId,
            int workingSeasonId)
        {
            Date = date;
            Type = type;
            PricePerUnit = pricePerUnit;
            Quantity = quantity;
            Sum = pricePerUnit * quantity;
            ArticleId = articleId;
            WorkingSeasonId = workingSeasonId;
            ExpenseByArableLands = new List<ExpenseByArableLand>();
        }
        public DateTime Date { get; set; }

        public ExpenseType Type { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal Quantity { get; set; }

        public decimal Sum { get; set; }
        
        public int? ArticleId { get; set; }

        public Article? Article { get; set; }

        public int WorkingSeasonId { get; set; }

        public int TenantId { get; set; }

        public List<ExpenseByArableLand> ExpenseByArableLands { get; set; }

        public Expense AddExpenseByArableLands(List<ExpenseByArableLand> arableLands)
        {
            ExpenseByArableLands = arableLands;
            return this;
        }
    }
}
