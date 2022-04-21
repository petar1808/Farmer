using Application.Mappings;
using Application.Models.Seedings;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Seedings
{
    public class SeedingsGetViewModel : IMapFrom<GetSeedingModel>
    {
        public int Id { get; init; }

        public int ArableLandId { get; set; }

        public string AreableLandName { get; init; } = default!;

        public int ArticleId { get; set; }

        public string ArticleName { get; init; } = default!;

        public int SizeInDecar { get; init; } = default!;

        public string SeasonName { get; init; } = default!;

        public DateTime? StartDate { get; init; }

        public DateTime? EndDate { get; init; }
    }
}
