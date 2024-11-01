using Application.Services;
using FluentValidation;

namespace Application.Features.Expenses.Commands.Create
{
    public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
    {
        public CreateExpenseCommandValidator(IFarmerDbContext farmerDbContext)
        {
            this.RuleFor(x => x.Quantity)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Количеството трябва да е положително число");

            this.RuleFor(x => x.PricePerUnit)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Цената за единица трябва да е положително число");

            this.RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Датата е задължителна");

            this.RuleFor(x => x.WorkingSeasonId)
                .Must(id =>
                {
                    var seeding = farmerDbContext
                        .WorkingSeasons
                        .Any(x => x.Id == id);

                    return seeding;
                })
                .WithMessage(x => $"Сеитба с Ид: {x.WorkingSeasonId} не съществува!");

            this.RuleFor(x => x.ArticleId)
                .Must(id =>
                {
                    if (id is not null)
                    {
                        return farmerDbContext.Articles.Any(x => x.Id == id);
                    }
                    return true;
                })
                .WithMessage(x => $"Артикул с Ид: {x.ArticleId} не съществува!");
        }
    }
}
