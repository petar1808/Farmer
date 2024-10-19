namespace WebUI.ServicesModel.Expenses
{
    public class DetailsExpenseModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Type { get; set; }

        private decimal _sum;
        public decimal Sum
        {
            get => _sum;
            set 
            {
                _pricePerUnit = value;
                _quantity = 1;
                _sum = value;
            }
        }

        private decimal _pricePerUnit;
        public decimal PricePerUnit
        {
            get => _pricePerUnit;
            set
            {
                _pricePerUnit = value;
                _sum = _pricePerUnit * _quantity;
            }
        }

        private decimal _quantity;
        public decimal Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                _sum = _pricePerUnit * _quantity;
            }
        }

        public int? ArticleId { get; set; }

        public int WorkingSeasonId { get; set; }

        public IEnumerable<int> SelectedArableLands { get; set; } = Enumerable.Empty<int>();
    }
}
