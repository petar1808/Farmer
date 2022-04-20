using Application.Mappings;
using Application.Models.Users;

namespace Web.Areas.Identity.ViewModels
{
    public class EditUserViewModel : IMapFrom<GetUserModel>
    {
        public string Id { get; set; } = default!;

        public string Email { get; init; } = default!;

        public bool Active { get; init; } = default!;
    }
}
