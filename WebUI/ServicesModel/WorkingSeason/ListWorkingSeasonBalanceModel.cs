namespace WebUI.ServicesModel.WorkingSeason
{
    public class ListWorkingSeasonBalanceModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Income { get; set; }

        public decimal Expenses { get; set; }

        public decimal Profit { get; set; }
    }
}
