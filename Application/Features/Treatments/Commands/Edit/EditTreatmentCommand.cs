using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Treatments.Commands.Edit
{
    public class EditTreatmentCommand : CommonTreatmentInputCommandModel, IRequest<Result>
    {
        public int Id { get; set; }

        public class EditTreatmentCommandHandler : IRequestHandler<EditTreatmentCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public EditTreatmentCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                EditTreatmentCommand request,
                CancellationToken cancellationToken)
            {
                var treatment = await farmerDbContext
                  .Treatments
                  .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (treatment == null)
                {
                    return $"Третиране с Ид: {request.Id} не съществува!";
                }

                treatment
                    .UpdateDate(request.Date)
                    .UpdateТreatmentType(request.TreatmentType)
                    .UpdateAmountOfFuel(request.AmountOfFuel)
                    .UpdateFuelPrice(request.FuelPrice)
                    .UpdateArticle(request.ArticleId)
                    .UpdateArticleQuantity(request.ArticleQuantity)
                    .UpdateArticlePrice(request.ArticlePrice);

                farmerDbContext.Update(treatment);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
