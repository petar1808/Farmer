namespace WebUI.ServicesModel.Identity
{
    public class ChangePasswordModel
    {
        public string Email { get; set; } = default!;

        public string CurrentPassword { get; set; } = default!;

        public string NewPassword { get; set; } = default!;

        public string RepeatPassword { get; set; } = default!;
    }
}
