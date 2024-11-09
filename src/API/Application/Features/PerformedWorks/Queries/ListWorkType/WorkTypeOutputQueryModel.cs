using Application.Models;

namespace Application.Features.PerformedWorks.Queries.ListWorkType
{
    public class WorkTypeOutputQueryModel : SelectionListModel
    {
        public WorkTypeOutputQueryModel(
           int value,
           string name) : base(value, name)
        {
        }
    }
}
