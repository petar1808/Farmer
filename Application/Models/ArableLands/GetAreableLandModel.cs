using Application.Mappings;
using AutoMapper;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.ArableLands
{
    public class GetAreableLandModel : ArablaLandBaseModel , IMapFrom<ArableLand> 
    {
        public int Id { get; init; }
    }
}
