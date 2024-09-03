using Application.Services;
using FluentValidation;

namespace Application.Features.ArableLands.Commands.Create
{
    public class CreateArableLandCommandValidator : AbstractValidator<CreateArableLandCommand>
    {
        public CreateArableLandCommandValidator(IFarmerDbContext farmerDbContext)
        {
            this.RuleFor(x => x.Name)
                .Length(2, 50)
                .WithMessage("Името трябва да е между 2 и 50 символа");

            this.RuleFor(x => x.SizeInDecar)
                .ExclusiveBetween(0, int.MaxValue)
                .WithMessage("Декарите трябва да са положително число");

            this.RuleFor(x => x.Name)
                .Must((nameValue) =>
                {
                    var arableLandUnique = farmerDbContext
                        .ArableLands
                        .Any(x => x.Name == nameValue);

                    return !arableLandUnique;
                })
                .WithMessage("Има създадена земя със същото име");
        }
    }
}
