using Application.Mappings;
using AutoMapper;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Seedings
{
    public class AddSeedingModel : IMapFrom<Seeding>
    {
        [Display(Name = "Arable Land Name")]
        public int ArableLandId { get; init; }

        public IEnumerable<SelectedListArableLand> ArableLands { get; set; } = default!;

        [Display(Name = "Article Name")]
        public int ArticleId { get; init; }

        public IEnumerable<SelectedListArticle> Articles { get; set; } = default!;

        [Display(Name = "Woriking Season Name")]
        public int WorkingSeasonId { get; init; }


        public virtual void Mapping(Profile mapper)
            => mapper.CreateMap<Seeding, AddSeedingModel>()
                .ForMember(x => x.ArableLandId, cfg => cfg.MapFrom(c => c.ArableLandId));
    }
}
