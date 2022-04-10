using Application.Mappings;
using Application.Models.ArableLands;
using AutoMapper;

namespace Web.ViewModels.ArableLands
{
    public class GetArableLandViewModel : IMapFrom<GetAreableLandModel>
    {
        public int Id { get; init; }

        public string Name { get; init; } = default!;

        public int SizeInDecar { get; init; }

        //public virtual void Mapping(Profile mapper)
        //    => mapper.CreateMap<GetAreableLandModel, GetArableLandViewModel>();
    }
}
