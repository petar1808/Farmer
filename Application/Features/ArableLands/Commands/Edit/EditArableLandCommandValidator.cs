using Application.Features.ArableLands.Commands.Create;
using Application.Services;
using FluentValidation;

namespace Application.Features.ArableLands.Commands.Edit
{
    public class EditArableLandCommandValidator : AbstractValidator<EditArableLandCommand>
    {
        public EditArableLandCommandValidator(IFarmerDbContext farmerDbContext)
        {

            this.RuleFor(x => x.Name)
                .Length(2, 50)
                .WithMessage("Името трябва да е между 2 и 50 символа");

            this.RuleFor(x => x.SizeInDecar)
                .ExclusiveBetween(0, int.MaxValue)
                .WithMessage("Декарите трябва да са положително число");
        }
    }
}
