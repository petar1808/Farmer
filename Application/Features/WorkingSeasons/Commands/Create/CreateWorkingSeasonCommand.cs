using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WorkingSeasons.Commands.Create
{
    public class CreateWorkingSeasonCommand : CommonWorkingSeasonInputComandModel, IRequest<Result>
    {
        public class CreateWorkingSeasonCommandHandler : IRequestHandler<CreateWorkingSeasonCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public CreateWorkingSeasonCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                CreateWorkingSeasonCommand request,
                CancellationToken cancellationToken)
            {
                var workingSeasonDate = await farmerDbContext
                .WorkingSeasons
                .AnyAsync(x => x.StartDate.Year == request.StartDate.Year && x.EndDate.Year == request.EndDate.Year, cancellationToken);

                if (workingSeasonDate)
                {
                    return "Има съзаден сезон със същото начало и край";
                }

                var workingSeason = new WorkingSeason(
                    request.Name,
                    request.StartDate,
                    request.EndDate);

                await farmerDbContext.AddAsync(workingSeason, cancellationToken);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
