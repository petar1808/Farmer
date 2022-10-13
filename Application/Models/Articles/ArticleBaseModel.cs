using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.CommonConstraints;

namespace Application.Models.Articles
{
    public class ArticleBaseModel
    {
        [Required]
        [MaxLength(MaxNameLenght)]
        public string Name { get; init; } = default!;

        [Required]
        [EnumDataType(typeof(ArticleType))]
        public ArticleType ArticleType { get; init; }
    }
}
