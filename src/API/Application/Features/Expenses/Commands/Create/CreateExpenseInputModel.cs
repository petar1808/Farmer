namespace Application.Features.Expenses.Commands.Create
{
    public class CreateExpenseInputModel : CommonExpenseInputModel
    {
        public int WorkingSeasonId { get; set; }
    }
}
