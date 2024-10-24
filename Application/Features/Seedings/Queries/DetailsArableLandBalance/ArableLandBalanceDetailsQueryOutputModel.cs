using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Seedings.Queries.DetailsArableLandBalance
{
    public class ArableLandBalanceDetailsQueryOutputModel : IMapFrom<Seeding>
    {
        public decimal Income { get; private set; }

        public decimal Expenses { get; private set; }

        public decimal Profit => Income - Expenses;

        public virtual void Mapping(Profile mapper)
           => mapper.CreateMap<Seeding, ArableLandBalanceDetailsQueryOutputModel>()
                .ForMember(x => x.Income, cfg => cfg.MapFrom(c => ((c.HarvestedQuantityPerDecare * c.HarvestedGrainSellingPricePerKilogram) * c.ArableLand.SizeInDecar)))
                .ForMember(x => x.Expenses, cfg => cfg.MapFrom(c => CalculateExpenses(c)));

        private decimal? CalculateExpenses(Seeding seeding)
        {
            var seedingSummaryExpensesSum = seeding.ArableLand.SizeInDecar * (seeding.SeedsPricePerKilogram * seeding.SeedsQuantityPerDecare);

            //var performedWorkExpensesSum = seeding.PerformedWorks.Sum(x => (x.AmountOfFuel * x.FuelPrice));

            //var treatmentExpensesSum = seeding.Treatments.Sum(x => (x.AmountOfFuel * x.FuelPrice) + (x.ArticlePrice * x.ArticleQuantity * seeding.ArableLand.SizeInDecar));

            var expensesForHarvesting = seeding.ExpensesForHarvesting;

            var totalExpenses = seedingSummaryExpensesSum + expensesForHarvesting;

            return totalExpenses;
        }
    }
}
