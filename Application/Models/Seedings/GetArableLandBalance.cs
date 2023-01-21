using Application.Mappings;
using AutoMapper;
using Domain.Models;
using System.Text.Json.Serialization;

namespace Application.Models.Seedings
{
    public class GetArableLandBalance : IMapFrom<Seeding>
    {
        public decimal Income { get; private set; }

        public decimal Expenses => PerformedWorkExpenses + TreatmentFuelExpenses + TreatmentArticleExpenses + SeedingSummaryExpenses + ExpensesForHarvesting;

        public decimal Profit => Income - Expenses;

        private decimal PerformedWorkExpenses { get; set; }

        private decimal TreatmentFuelExpenses { get; set; }

        private decimal TreatmentArticleExpenses { get; set; }

        private decimal SeedingSummaryExpenses { get; set; }

        private decimal ExpensesForHarvesting { get; set; }

        public virtual void Mapping(Profile mapper)
           => mapper.CreateMap<Seeding, GetArableLandBalance>()
                .ForMember(x => x.Income, cfg => cfg.MapFrom(c => ((c.HarvestedQuantityPerDecare * c.HarvestedGrainSellingPricePerKilogram) * c.ArableLand.SizeInDecar) + c.Subsidies.Sum(x => x.Income)))
                .ForMember(x => x.PerformedWorkExpenses, cfg => cfg.MapFrom(seeding => seeding.PerformedWorks.Sum(x => (x.AmountOfFuel * x.FuelPrice))))
                .ForMember(x => x.TreatmentFuelExpenses, cfg => cfg.MapFrom(seeding => seeding.Treatments.Sum(x => (x.AmountOfFuel * x.FuelPrice))))
                .ForMember(x => x.TreatmentArticleExpenses, cfg => cfg.MapFrom(seeding => seeding.Treatments.Sum(x => x.ArticlePrice * x.ArticleQuantity) * seeding.ArableLand.SizeInDecar))
                .ForMember(x => x.SeedingSummaryExpenses, cfg => cfg.MapFrom(seeding => seeding.ArableLand.SizeInDecar * (seeding.SeedsPricePerKilogram * seeding.SeedsQuantityPerDecare)))
                .ForMember(x => x.ExpensesForHarvesting, cfg => cfg.MapFrom(seeding => seeding.ExpensesForHarvesting));
    }
}
