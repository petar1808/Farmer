using Application.Mappings;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Seedings
{
    public class GetSeedingSummaryModel : SeedingSummaryBaseModel, IMapFrom<Seeding>
    {
        public string ArticleName { get; init; } = default!;

        public decimal IncomeFromHarvestedGrains { get; private set; }

        public decimal Income => IncomeFromHarvestedGrains + SubsidiesIncome;

        public decimal Expenses { get; private set; }

        public decimal Profit => (IncomeFromHarvestedGrains + SubsidiesIncome) - Expenses;

        public virtual void Mapping(Profile mapper)
           => mapper.CreateMap<Seeding, GetSeedingSummaryModel>()
                .ForMember(x => x.ArticleName, cfg => cfg.MapFrom(c => c.Article.Name))
                .ForMember(x => x.ArticleId, cfg => cfg.MapFrom(c => c.Article.Id))
                
                .ForMember(x => x.IncomeFromHarvestedGrains, cfg => cfg.MapFrom(c => (c.HarvestedQuantityPerDecare * c.HarvestedGrainSellingPricePerKilogram) * c.ArableLand.SizeInDecar))
                .ForMember(x => x.Expenses, cfg => cfg.MapFrom(c => CalculateExpenses(c)));


        private decimal? CalculateExpenses(Seeding seeding)
        {
            var seedingSummaryExpensesSum = seeding.ArableLand.SizeInDecar * (seeding.SeedsPricePerKilogram * seeding.SeedsQuantityPerDecare);

            var performedWorkExpensesSum = seeding.PerformedWorks.Sum(x => (x.AmountOfFuel * x.FuelPrice));

            var treatmentExpensesSum = seeding.Treatments.Sum(x => (x.AmountOfFuel * x.FuelPrice) + (x.ArticlePrice * x.ArticleQuantity * seeding.ArableLand.SizeInDecar));

            var expensesForHarvesting = seeding.ExpensesForHarvesting;

            var totalExpenses =  performedWorkExpensesSum + treatmentExpensesSum + seedingSummaryExpensesSum + expensesForHarvesting;

            return totalExpenses;
        }
    }
}
