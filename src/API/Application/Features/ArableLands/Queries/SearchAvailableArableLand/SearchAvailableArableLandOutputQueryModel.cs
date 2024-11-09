using Application.Models;

namespace Application.Features.ArableLands.Queries.SearchAvailableArableLand
{
    public class SearchAvailableArableLandOutputQueryModel : SelectionListModel
    {
        public SearchAvailableArableLandOutputQueryModel(
            int value,
            string name) : base(value, name)
        {
        }
    }
}
