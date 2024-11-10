using Domain.Enum;
using System.Diagnostics.CodeAnalysis;

namespace Domain
{
    [ExcludeFromCodeCoverage]
    public class ExpensesConfigurations
    {
        public ExpenseType ExpenseType { get; set; }

        public bool DistributeByArableLand { get; set; }

        public ArticleType? ArticleType { get; set; }

        public bool ShowPricePerUnit { get; set; }
    }
}
