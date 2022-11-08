using Application.Mappings;
using Application.Models.Seedings;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.ModelConstraint.CommonConstraints;

namespace Application.Models.WorkingSeasons
{
    public class ListWorkingSeasonBalanceModel : IMapFrom<WorkingSeason>
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(MaxNameLenght)]
        public string Name { get; init; } = default!;

        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; init; }

        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; init; }

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
