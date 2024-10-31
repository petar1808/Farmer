using Application.Mappings;
using Domain.Models;


namespace Application.Features.Expenses.Queries
{
    public class CommonExpensesQueryOutputModel : IMapFrom<Expense>
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }
    }
}
