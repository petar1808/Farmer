namespace Application.Features.Identity.Commands.ChangePassword
{
    public class ChangePasswordInputCommandModel
    {
        public string Email { get; set; } = default!;

        public string CurrentPassword { get; set; } = default!;

        public string NewPassword { get; set; } = default!;
    }
}
