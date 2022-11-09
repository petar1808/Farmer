using Application.Mappings;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Seedings
{
    public class SeedingSummaryBaseModel : IMapFrom<Seeding>
    {
        public int Id { get; set; }

        public int? ArticleId { get; init; }

        [Range(0, int.MaxValue, ErrorMessage = "Засято семе на декар трябва да е положително число")]
        public int SeedsQuantityPerDecare { get; init; }

        [Range(0d, int.MaxValue, ErrorMessage = "Цената на засятото семе трябва да е положително число")]
        public decimal SeedsPricePerKilogram { get; init; }

        [Range(0, int.MaxValue, ErrorMessage = "Ожънатото количество на декар трябва да е положително число")]
        public int HarvestedQuantityPerDecare { get; init; }

        [Range(0d, int.MaxValue, ErrorMessage = "Продажната цена зърното трябва да е положително число")]
        public decimal HarvestedGrainSellingPricePerKilogram { get; init; }

        [Range(0d, int.MaxValue, ErrorMessage = "Субсидиите трябва да са положително число")]
        public decimal SubsidiesIncome { get; init; }

        [Range(0d, int.MaxValue, ErrorMessage = "Разходите за жътва трябва да са положително число")]
        public decimal ExpensesForHarvesting { get;  init; }
    }
}
