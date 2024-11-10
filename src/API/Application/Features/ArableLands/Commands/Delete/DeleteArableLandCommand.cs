using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ArableLands.Commands.Delete
{
    public class DeleteArableLandCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public class DeleteArableLandCommandHandler : IRequestHandler<DeleteArableLandCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public DeleteArableLandCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                DeleteArableLandCommand request,
                CancellationToken cancellationToken)
            {
                var arableLand = await farmerDbContext
                .ArableLands
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (arableLand == null)
                {
                    return $"Земя с Ид: {request.Id} не съществува!";
                }

                var seeding = await farmerDbContext
                    .Seedings
                    .AnyAsync(x => x.ArableLandId == request.Id, cancellationToken);

                if (seeding)
                {
                    return $"Земя с Ид: {request.Id} не може да бъде изтрита, защото имате данни в сеитбата с тази земя!";
                }

                farmerDbContext.Remove(arableLand);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
