using Application.Mappings;
using Application.Models.Articles;
using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.CommonConstraints;

namespace Web.ViewModels.Articles
{
    public class EditArticleViewModel : ArticleBaseModel, IMapFrom<GetArticleModel>
    {
        [Display(Name = "Ид")]
        public int Id { get; init; }
    }
}
