namespace WebUI.ServicesModel.Identity
{
    public class ForgotPasswordModel
    {
        public string Email { get; set; } = default!;

        public string ChangePasswordUrl { get; set; } = default!;
    }
}
