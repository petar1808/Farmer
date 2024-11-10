namespace Application.Features.Identity.Commands.ForgotPassword
{
    public class ForgotPasswordInputCommandModel
    {
        public string Email { get; set; } = default!;

        public string ChangePasswordUrl { get; set; } = default!;
    }
}
