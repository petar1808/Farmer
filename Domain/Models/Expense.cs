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
        public DateTime Date { get; private set; }

        public ExpenseType Type { get; private set; }

        public decimal PricePerUnit { get; private set; }

        public decimal Quantity { get; private set; }

        public decimal Sum { get; private set; }

        public int? ArticleId { get; private set; }

        public Article? Article { get; private set; }

        public int WorkingSeasonId { get; private set; }

        public int TenantId { get; set; }

        public List<ExpenseByArableLand> ExpenseByArableLands { get; set; }

        public Expense AddExpenseByArableLands(List<ExpenseByArableLand> arableLands)
        {
            ExpenseByArableLands = arableLands;
            return this;
        }

        public Expense UpdateExpenseByArableLands(List<ExpenseByArableLand> newArableLands)
        {
            // Remove arable lands that are no longer in the new list
            var arableLandsToRemove = ExpenseByArableLands
                .Where(existing => !newArableLands.Any(newLand => newLand.ArableLandId == existing.ArableLandId))
                .ToList();

            foreach (var arableLand in arableLandsToRemove)
            {
                ExpenseByArableLands.Remove(arableLand);
            }

            // Add new arable lands that are not in the existing list
            var arableLandsToAdd = newArableLands
                .Where(newLand => !ExpenseByArableLands.Any(existing => existing.ArableLandId == newLand.ArableLandId))
                .ToList();

            foreach (var arableLand in arableLandsToAdd)
            {
                ExpenseByArableLands.Add(arableLand);
            }

            // Update existing arable lands
            foreach (var existingArableLand in ExpenseByArableLands)
            {
                var matchingNewArableLand = newArableLands.FirstOrDefault(newLand => newLand.ArableLandId == existingArableLand.ArableLandId);
                if (matchingNewArableLand != null)
                {
                    existingArableLand.UpdateSum(matchingNewArableLand.Sum);
                }
            }

            return this;
        }


        public Expense UpdateDate(DateTime date)
        {
            Date = date;
            return this;
        }

        public Expense UpdateType(ExpenseType type)
        {
            this.Type = type;
            return this;
        }

        public Expense UpdateArticleId(int? articleId)
        {
            ArticleId = articleId;
            return this;
        }

        public Expense UpdatePricePerUnit(decimal pricePerUnit)
        {
            PricePerUnit = pricePerUnit;
            UpdateSum();  // Ensure sum is recalculated
            return this;
        }

        public Expense UpdateQuantity(decimal quantity)
        {
            Quantity = quantity;
            UpdateSum();  // Ensure sum is recalculated
            return this;
        }

        private void UpdateSum()
        {
            Sum = PricePerUnit * Quantity;
        }
    }
}
