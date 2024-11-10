using Application.Services;
using FluentValidation;

namespace Application.Features.Expenses.Commands.Edit
{
    public class EditExpenseCommandValidator : AbstractValidator<EditExpenseCommand>
    {
        public EditExpenseCommandValidator(IFarmerDbContext farmerDbContext)
        {
            this.RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Ид на разхода е задължително");

            this.RuleFor(x => x.Quantity)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Количеството трябва да е положително число");

            this.RuleFor(x => x.PricePerUnit)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Цената за единица трябва да е положително число");

            this.RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Датата е задължителна");

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
