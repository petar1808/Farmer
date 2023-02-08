namespace Application.Features.Identity.Commands.CreatePassword
{
    public class CreatePasswordInputCommandModel
    {
        public string Email { get; set; } = default!;

        public string Token { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
