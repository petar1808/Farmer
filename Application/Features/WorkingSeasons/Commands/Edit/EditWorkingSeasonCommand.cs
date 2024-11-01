using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WorkingSeasons.Commands.Edit
{
    public class EditWorkingSeasonCommand : CommonWorkingSeasonInputComandModel, IRequest<Result>
    {
        public int Id { get; set; }

        public class EditWorkingSeasonCommandHandler : IRequestHandler<EditWorkingSeasonCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public EditWorkingSeasonCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                EditWorkingSeasonCommand request,
                CancellationToken cancellationToken)
            {
                var workingSeason = await farmerDbContext
                    .WorkingSeasons
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (workingSeason == null)
                {
                    return $"Сезон с Ид: {request.Id} не съществува!";
                }

                if (request.StartDate.Year != workingSeason.StartDate.Year && request.EndDate.Year != workingSeason.EndDate.Year)
                {
                    var workingSeasonDate = await farmerDbContext
                    .WorkingSeasons
                    .AnyAsync(x => x.StartDate.Year == request.StartDate.Year && x.EndDate.Year == request.EndDate.Year, cancellationToken);

                    if (workingSeasonDate)
                    {
                        return "Има съзаден сезон със същото начало и край";
                    }
                }

                workingSeason
                    .UpdateName(request.Name)
                    .UpdateSratDate(request.StartDate)
                    .UpdateEndDate(request.EndDate);

                this.farmerDbContext.Update(workingSeason);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
