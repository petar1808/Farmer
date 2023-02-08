using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Subsidies.Commands.Edit
{
    public class EditSubsidyCommand : CommonSubsidyInputCommandModel, IRequest<Result>
    {
        public int Id { get; set; }

        public class EditSubsidyCommandHandler : IRequestHandler<EditSubsidyCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public EditSubsidyCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                EditSubsidyCommand request,
                CancellationToken cancellationToken)
            {
                var subsidy = await farmerDbContext
                .Subsidies
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (subsidy == null)
                {
                    return $"Субсидия с Ид: {request.Id} не съществува";
                }

                subsidy
                    .UpdateIncome(request.Income)
                    .UpdateDate(request.Date);

                farmerDbContext.Update(subsidy);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
