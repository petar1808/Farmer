using Application.Mappings;
using Domain.Models;

namespace Application.Features.ArableLands.Queries
{
    public class CommonArableLandOutputQueryModel : IMapFrom<ArableLand>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int SizeInDecar { get; set; }
    }
}
