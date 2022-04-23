using Application.Mappings;
using AutoMapper;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.ArableLands
{
    public class GetAreableLandModel : IMapFrom<ArableLand>
    {
        public int Id { get; init; }

        public string Name { get; init; } = default!;

        [Display(Name = "Size In Decar")]
        public int SizeInDecar { get; init; }
    }
}
