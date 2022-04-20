using Infrastructure.Enum;

namespace Web.Areas.Identity.ViewModels
{
    public class AddUserViewModel
    {
        public string Email { get; init; } = default!;

        public bool Active { get; init; } = default!;

        public string Password { get; init; } = default!;

        public UserRoles Role { get; init; } = default!;
    }
}
