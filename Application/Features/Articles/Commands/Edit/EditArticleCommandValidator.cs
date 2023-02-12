using FluentValidation;

namespace Application.Features.Articles.Commands.Edit
{
    public class EditArticleCommandValidator : AbstractValidator<EditArticleCommand>
    {
        public EditArticleCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Името е задължително")
                .Length(2, 50)
                .WithMessage("Името трябва да е между 2 и 50 символа");

            this.RuleFor(x => x.ArticleType)
                .IsInEnum()
                .WithMessage("Типът артикул е невалиден");
        }
    }
}
