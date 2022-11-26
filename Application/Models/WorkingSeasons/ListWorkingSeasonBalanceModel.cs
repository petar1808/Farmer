using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Models.WorkingSeasons
{
    public class ListWorkingSeasonBalanceModel : WorkingSeasonBaseModel, IMapFrom<WorkingSeason>
    {
        public int Id { get; init; }

        public decimal Income { get; private set; }

        public decimal Expenses { get; private set; }

        public decimal Profit => Income - Expenses;

        public virtual void Mapping(Profile mapper)
          => mapper.CreateMap<WorkingSeason, ListWorkingSeasonBalanceModel>()
               .ForMember(x => x.Income, cfg => cfg.MapFrom(c => (c.Seedings.Sum(x => (x.HarvestedQuantityPerDecare * x.HarvestedGrainSellingPricePerKilogram) * x.ArableLand.SizeInDecar))))
               .ForMember(x => x.Expenses, cfg => cfg.MapFrom(c => CalculateExpenses(c)));

        private decimal? CalculateExpenses(WorkingSeason workingSeason)
        {
            var seedingSummaryExpensesSum = workingSeason.Seedings.Sum(x => x.ArableLand.SizeInDecar * (x.SeedsPricePerKilogram * x.SeedsQuantityPerDecare));

            var performedWorkExpensesSum = workingSeason.Seedings.Sum(x => x.PerformedWorks.Sum(c => c.AmountOfFuel * c.FuelPrice));

            var treatmentExpensesSum = workingSeason.Seedings.Sum(x => x.Treatments.Sum(c => (c.AmountOfFuel * c.FuelPrice) + (c.ArticlePrice * c.ArticleQuantity * x.ArableLand.SizeInDecar)));

            var expensesForHarvesting = workingSeason.Seedings.Sum(x => x.ExpensesForHarvesting);

            var totalExpenses = performedWorkExpensesSum + treatmentExpensesSum + seedingSummaryExpensesSum + expensesForHarvesting;

            return totalExpenses;
        }
    }
}
