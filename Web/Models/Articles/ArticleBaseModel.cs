using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.CommonConstraints;

namespace Web.Models.Articles
{
    public class ArticleBaseModel
    {
        [Required]
        [StringLength(NameLenght)]
        public string Name { get; init; } = default!;

        [Required]
        [Display(Name = "Article Type")]
        public ArticleType ArticleType { get; init; }
    }
}
