using Application.Mappings;
using Application.Models.ArableLands;
using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.CommonConstraints;

namespace Web.ViewModels.ArableLands
{
    public class EditArableLandViewModel : ArableLandBaseViewModel, IMapFrom<GetAreableLandModel>
    {
        public int Id { get; init; }
    }
}
