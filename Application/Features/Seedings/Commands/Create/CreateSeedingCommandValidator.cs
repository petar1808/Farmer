using FluentValidation;

namespace Application.Features.Seedings.Commands.Create
{
    public class CreateSeedingCommandValidator : AbstractValidator<CreateSeedingCommand>
    {
        public CreateSeedingCommandValidator()
        {
            this.RuleFor(x => x.ArableLandId).NotEmpty().WithMessage("Земята е задължителна");

            this.RuleFor(x => x.WorkingSeasonId).NotEmpty().WithMessage("Сезона е задължителен");
        }
    }
}
