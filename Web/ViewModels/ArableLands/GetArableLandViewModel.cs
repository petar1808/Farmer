using Application.Mappings;
using Application.Models.ArableLands;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.ArableLands
{
    public class GetArableLandViewModel : ArableLandBaseViewModel, IMapFrom<GetAreableLandModel>
    {
        [Display(Name = "Ид")]
        public int Id { get; init; }

        //public virtual void Mapping(Profile mapper)
        //    => mapper.CreateMap<GetAreableLandModel, GetArableLandViewModel>();
    }
}
