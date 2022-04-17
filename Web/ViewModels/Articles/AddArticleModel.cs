using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.CommonConstraints;

namespace Web.ViewModels.Articles
{
    public class AddArticleModel
    {
        [Required]
        [StringLength(MaxNameLenght)]
        public string Name { get; init; } = default!;

        [Required]
        [Display(Name = "Article Type")]
        [EnumDataType(typeof(ArticleType))]
        public ArticleType ArticleType { get; init; }
    }
}
