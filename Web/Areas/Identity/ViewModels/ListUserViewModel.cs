using Application.Mappings;
using Application.Models.Users;

namespace Web.Areas.Identity.ViewModels
{
    public class ListUserViewModel :  IMapFrom<ListUserModel>
    {
        public string Id { get; init; } = default!;

        public string Email { get; init; } = default!;

        public string Role { get; init; } = default!;

        public bool Active { get; init; } = default!;
    }
}
