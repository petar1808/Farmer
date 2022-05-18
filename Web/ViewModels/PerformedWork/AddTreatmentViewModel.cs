using Application.Models.Common;

namespace Web.ViewModels.PerformedWork
{
    public class AddTreatmentViewModel : AddBaseModel
    {
        public int ArticleId { get; init; }

        public IEnumerable<SelectionListModel> Articles { get; set; } = default!;
    }
}
