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
    public class SeedingSummaryBaseModel : IMapFrom<Seeding>
    {
        public int Id { get; set; }

        public int? ArticleId { get; init; }

        public int SeedsQuantityPerDecare { get; init; }

        public decimal SeedsPricePerKilogram { get; init; }

        public int HarvestedQuantityPerDecare { get; init; }

        public decimal HarvestedGrainSellingPricePerKilogram { get; init; }

        public decimal SubsidiesIncome { get; init; }

        public decimal ExpensesForHarvesting { get;  init; }
    }
}
