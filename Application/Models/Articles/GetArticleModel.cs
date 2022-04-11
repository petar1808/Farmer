using Application.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Articles
{
    public class GetArticleModel : ArticleBaseModel, IMapFrom<Article>
    {
        [Required]
        public int Id { get; init; }
    }
}
