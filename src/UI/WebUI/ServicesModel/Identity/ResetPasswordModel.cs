namespace WebUI.ServicesModel.Identity
{
    public class ResetPasswordModel
    {
        public string Email { get; set; } = default!;

        public string NewPassword { get; set; } = default!;

        public string RepeatPassword { get; set; } = default!;

        public string Token { get; set; } = default!;
    }
}
