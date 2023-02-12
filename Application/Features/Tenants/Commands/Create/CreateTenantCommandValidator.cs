using FluentValidation;

namespace Application.Features.Tenants.Commands.Create
{
    public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantCommandValidator() 
        {
            this.RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Името на организацията е задължително")
                .Length(2, 50)
                .WithMessage("Името на организацията трябва да е между 2 и 50 символа");
        }
    }
}
