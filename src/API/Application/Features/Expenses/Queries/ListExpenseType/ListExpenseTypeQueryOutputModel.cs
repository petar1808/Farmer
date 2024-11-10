using Application.Models;

namespace Application.Features.Expenses.Queries.ListExpenseType
{
    public class ListExpenseTypeQueryOutputModel : SelectionListModel
    {
        public ListExpenseTypeQueryOutputModel(
           int value,
           string name) : base(value, name)
        {

        }
    }
}
