using Application.Mappings;
using Domain.Enum;
using Domain.Models;

namespace Application.Features.Articles.Queries.Details
{
    public class ArticleDetailsQueryOutputModel : CommonArticleOutputQueryModel, IMapFrom<Article>
    {
        public ArticleType ArticleType { get; set; }
    }
}
