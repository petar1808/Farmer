using Application.Models;

namespace Application.Features.Articles.Queries
{
    public class CommonArticleTypeOutputQueryModel : SelectionListModel
    {
        public CommonArticleTypeOutputQueryModel(
            int value,
            string name) : base(value, name)
        {
        }
    }
}
