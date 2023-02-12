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
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Името е задължително")
                .Length(2, 50)
                .WithMessage("Името трябва да е между 2 и 50 символа");

            this.RuleFor(x => x.ArticleType)
                .IsInEnum()
                .WithMessage("Типът артикул е невалиден");

            //this.RuleFor(x => x.Name)
            //    .MustAsync(async (command, nameValue, token) =>
            //    {
            //        var articleUnique = await farmerDbContext
            //                .Articles
            //                .AnyAsync(
            //                    x => x.Name == nameValue && x.ArticleType == command.ArticleType,
            //                    token);

            //        return articleUnique;

            //    }).WithMessage("Има създаден артикул със същото име и тип");

        }
    }
}
