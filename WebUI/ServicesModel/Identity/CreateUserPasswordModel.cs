namespace WebUI.ServicesModel.Identity
{
    public class CreateUserPasswordModel
    {
        public string Email { get; set; } = default!;

        public string Token { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
