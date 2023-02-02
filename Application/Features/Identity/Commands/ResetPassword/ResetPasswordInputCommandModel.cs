namespace Application.Features.Identity.Commands.ResetPassword
{
    public class ResetPasswordInputCommandModel
    {
        public string Email { get; init; } = default!;

        public string NewPassword { get; init; } = default!;

        public string Token { get; init; } = default!;
    }
}
