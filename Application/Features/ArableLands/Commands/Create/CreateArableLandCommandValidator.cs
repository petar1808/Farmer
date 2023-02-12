using FluentValidation;

namespace Application.Features.ArableLands.Commands.Create
{
    public class CreateArableLandCommandValidator : AbstractValidator<CreateArableLandCommand>
    {
        public CreateArableLandCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Името е задължително")
                .Length(2, 50)
                .WithMessage("Името трябва да е между 2 и 50 символа");

            this.RuleFor(x => x.SizeInDecar)
                .ExclusiveBetween(0, int.MaxValue)
                .WithMessage("Декарите трябва да са положително число");
        }
    }
}
