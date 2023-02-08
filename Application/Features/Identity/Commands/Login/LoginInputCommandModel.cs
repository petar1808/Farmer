namespace Application.Features.Identity.Commands.Login
{
    public class LoginInputCommandModel
    {
        public string? Email { get; set; } = default!;

        public string? Password { get; set; } = default!;

        public string? UserName { get; set; } = default!;
    }
}
