using Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Application.Features.Tenants.Commands.Create
{
    public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantCommandValidator(IFarmerDbContext farmerDbContext) 
        {
            this.RuleFor(x => x.Name)
                .Length(2, 50)
                .WithMessage("Името на организацията трябва да е между 2 и 50 символа");

            this.RuleFor(x => x.Name)
               .Must((nameValue) =>
               {
                   var tenantUnique = farmerDbContext
                        .Tenants
                        .Any(x => x.Name == nameValue);

                   return !tenantUnique;
               })
               .WithMessage("Има създадена организация със същото име");
        }
    }
}
