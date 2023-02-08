using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WorkingSeasons.Commands.Delete
{
    public class DeleteWorkingSeasonCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public class DeleteWorkingSeasonCommandHandler : IRequestHandler<DeleteWorkingSeasonCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public DeleteWorkingSeasonCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                DeleteWorkingSeasonCommand request,
                CancellationToken cancellationToken)
            {
                var workingSeason = await farmerDbContext
                .WorkingSeasons
                .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (workingSeason == null)
                {
                    return $"Сезон с Ид: {request} не съществува!";
                }

                var workingSeasonSeeding = await farmerDbContext
                    .Seedings
                    .AnyAsync(x => x.WorkingSeasonId == request.Id);

                if (workingSeasonSeeding)
                {
                    return $"Сезон с Ид: {request.Id} не може да бъде изтрит, защото е създадена сеитба за този сезон!";
                }

                farmerDbContext.WorkingSeasons.Remove(workingSeason);
                await farmerDbContext.SaveChangesAsync();

                return Result.Success;
            }
        }
    }
}
