using Application.Mappings;
using Application.Models.Articles;
using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.CommonConstraints;

namespace Web.ViewModels.Articles
{
    public class EditArticleViewModel : IMapFrom<GetArticleModel>
    {
        public int Id { get; init; }

        [Required]
        [StringLength(MaxNameLenght)]
        public string Name { get; init; } = default!;

        [Required]
        [Display(Name = "Article Type")]
        [EnumDataType(typeof(ArticleType))]
        public ArticleType ArticleType { get; init; }
    }
}
