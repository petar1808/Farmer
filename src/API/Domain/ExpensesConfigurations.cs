using Domain.Enum;

namespace Domain
{
    public class ExpensesConfigurations
    {
        public ExpenseType ExpenseType { get; set; }

        public bool DistributeByArableLand { get; set; }

        public ArticleType? ArticleType { get; set; }

        public bool ShowPricePerUnit { get; set; }
    }
}
