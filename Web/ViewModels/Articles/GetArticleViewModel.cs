using Application.Mappings;
using Application.Models.Articles;
using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Articles
{
    public class GetArticleViewModel : IMapFrom<GetArticleModel>
    {
        public int Id { get; init; }

        public string Name { get; init; } = default!;

        [Display(Name = "Article Type")]
        [EnumDataType(typeof(ArticleType))]
        public ArticleType ArticleType { get; init; }
    }
}
