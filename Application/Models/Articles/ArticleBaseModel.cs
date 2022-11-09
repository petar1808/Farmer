using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.CommonConstraints;

namespace Application.Models.Articles
{
    public class ArticleBaseModel
    {
        [Required(ErrorMessage = "Името е задължително")]
        [MaxLength(MaxNameLenght, ErrorMessage = "Името трябва да съдържа най-много 50 знака")]
        public string Name { get; init; } = default!;

        [Required(ErrorMessage = "Типът артикул е задължителен")]
        [EnumDataType(typeof(ArticleType))]
        public ArticleType ArticleType { get; init; }
    }
}
