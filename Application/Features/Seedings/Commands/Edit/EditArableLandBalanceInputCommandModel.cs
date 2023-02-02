using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Seedings.Commands.Edit
{
    public class EditArableLandBalanceInputCommandModel
    {
        public int SeedingId { get; private set; }

        public int? ArticleId { get; init; }

        public decimal SeedsQuantityPerDecare { get; init; }

        public decimal SeedsPricePerKilogram { get; init; }

        public int HarvestedQuantityPerDecare { get; init; }

        public decimal HarvestedGrainSellingPricePerKilogram { get; init; }

        public decimal ExpensesForHarvesting { get; init; }

        public void SetSeedingId(int seedingId)
        {
            SeedingId = seedingId;
        }
    }
}
