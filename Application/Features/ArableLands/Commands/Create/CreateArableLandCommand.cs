using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ArableLands.Commands.Create
{
    public class CreateArableLandCommand : CommonArableLandInputComandModel, IRequest<Result>
    {
        public class CreateArableLandCommandHandler : IRequestHandler<CreateArableLandCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public CreateArableLandCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                CreateArableLandCommand request,
                CancellationToken cancellationToken)
            {
                var arableLandUnique = await farmerDbContext
                .ArableLands
                .AnyAsync(x => x.Name == request.Name, cancellationToken);

                if (arableLandUnique)
                {
                    return "Има създадена земя със същото име";
                }

                var arableLand = new ArableLand(request.Name, request.SizeInDecar);

                await farmerDbContext.AddAsync(arableLand, cancellationToken);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
