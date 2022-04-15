using Application.Mappings;
using Domain.Enum;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Articles
{
    public class GetArticleModel : IMapFrom<Article>
    {
        public int Id { get; init; }

        public string Name { get; init; } = default!;

        public ArticleType ArticleType { get; init; }
    }
}
