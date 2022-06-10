using Application.Mappings;
using Domain.Enum;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Articles
{
    public class GetArticleModel : ArticleBaseModel, IMapFrom<Article>
    {
        public int Id { get; init; }
    }
}
