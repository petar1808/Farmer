using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Treatments.Commands.Create
{
    public class CreateTreatmentCommand : CommonTreatmentInputCommandModel, IRequest<Result>
    {
        public int SeedingId { get; private set; }

        public void SetSeedingId(int seedingId)
        {
            SeedingId = seedingId;
        }

        public class CreateTreatmentCommandHandler : IRequestHandler<CreateTreatmentCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public CreateTreatmentCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                CreateTreatmentCommand request,
                CancellationToken cancellationToken)
            {
                var seeding = await farmerDbContext
               .Seedings
               .AnyAsync(x => x.Id == request.SeedingId, cancellationToken);

                if (!seeding)
                {
                    return $"Сеитба с Ид: {request.SeedingId} не съществува!";
                }

                var article = await farmerDbContext
                    .Articles
                    .AnyAsync(x => x.Id == request.ArticleId, cancellationToken);

                if (!article)
                {
                    return $"Артикул с Ид: {request.ArticleId} не съществува!";
                }

                var treatment = new Treatment(request.Date,
                    request.TreatmentType,
                    request.AmountOfFuel,
                    request.FuelPrice,
                    request.ArticleId,
                    request.ArticleQuantity,
                    request.SeedingId,
                    request.ArticlePrice);

                await farmerDbContext.AddAsync(treatment, cancellationToken);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
