using Application.Services;
using Domain.Enum;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Application.Features.Articles.Commands.Create
{
    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator(IFarmerDbContext farmerDbContext)
        {
            this.RuleFor(x => x.Name)
                .Length(2, 50)
                .WithMessage("Името трябва да е между 2 и 50 символа");

            this.RuleFor(x => x.ArticleType)
                .IsInEnum()
                .WithMessage("Типът артикул е невалиден")
                .OverridePropertyName("Тип");

            this.RuleFor(x => x.Name)
                .Must((command, nameValue) =>
                {
                    var articleUnique = farmerDbContext
                            .Articles
                            .Any(
                                x => x.Name == nameValue && x.ArticleType == command.ArticleType);

                    return !articleUnique;
                })
                .WithMessage("Има създаден артикул със същото име и тип");
        }
    }
}
