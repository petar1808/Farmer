using Application.Services;
using FluentValidation;

namespace Application.Features.Articles.Commands.Edit
{
    public class EditArticleCommandValidator : AbstractValidator<EditArticleCommand>
    {
        public EditArticleCommandValidator(IFarmerDbContext farmerDbContext)
        {
            this.RuleFor(x => x.Name)
                .Length(2, 50)
                .WithMessage("Името трябва да е между 2 и 50 символа");

            this.RuleFor(x => x.ArticleType)
                .IsInEnum()
                .WithMessage("Типът артикул е невалиден");
        }
    }
}
