using Domain.Common;

namespace Domain.Models
{
    public class ExpenseByArableLand : Entity<int>
    {

        public ExpenseByArableLand(int arableLandId, decimal sum)
        {
            ArableLandId = arableLandId;
            Sum = sum;
            ArableLand = default!;
            Expense = default!;
        }
        public int ArableLandId { get; set; }

        public ArableLand ArableLand { get; set; }

        public decimal Sum { get; private set; }

        public int ExpenseId { get; set; }

        public Expense Expense { get; set; }

        public ExpenseByArableLand UpdateSum(decimal sum)
        {
            Sum = sum;
            return this;
        }
    }
}
