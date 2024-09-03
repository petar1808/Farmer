using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Features.WorkingSeasons.Queries.List
{
    public class WorkingSeasonBalanceListQueryOutputModel : CommonWorkingSeasonOutputQueryModel, IMapFrom<WorkingSeason>
    {
        public decimal Income { get; private set; }

        public decimal Expenses { get; private set; }

        public decimal Profit => Income - Expenses;

        public virtual void Mapping(Profile mapper)
          => mapper.CreateMap<WorkingSeason, WorkingSeasonBalanceListQueryOutputModel>()
               .ForMember(x => x.Income, cfg => cfg.MapFrom(c => c.Seedings.Sum(x => x.HarvestedQuantityPerDecare * x.HarvestedGrainSellingPricePerKilogram * x.ArableLand.SizeInDecar) + c.Seedings.Sum(x => x.Subsidies.Sum(s => s.Income))))
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
