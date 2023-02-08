using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PerformedWorks.Commands.Edit
{
    public class EditPerformedWorkCommand : CommonPerformedWorkInputCommandModel, IRequest<Result>
    {
        public int Id { get; set; }

        public class EditPerformedWorkCommandHandler : IRequestHandler<EditPerformedWorkCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public EditPerformedWorkCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                EditPerformedWorkCommand request,
                CancellationToken cancellationToken)
            {
                var performedWork = await farmerDbContext
                .PerformedWorks
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (performedWork == null)
                {
                    return $"Обработка с Ид: {request.Id} не съществува!";
                }

                performedWork
                    .UpdateWorkType(request.WorkType)
                    .UpdateDate(request.Date)
                    .UpdateAmountOfFuel(request.AmountOfFuel)
                    .UpdateFuelPrice(request.FuelPrice);

                farmerDbContext.Update(performedWork);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
