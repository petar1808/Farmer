using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Treatments.Commands.Delete
{
    public class DeleteTreatmentCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public class DeleteTreatmentCommandHandler : IRequestHandler<DeleteTreatmentCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public DeleteTreatmentCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                DeleteTreatmentCommand request,
                CancellationToken cancellationToken)
            {
                var treatment = await farmerDbContext
                .Treatments
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (treatment == null)
                {
                    return $"Третиране с Ид: {request.Id} не съществува!";
                }

                farmerDbContext.Remove(treatment);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
