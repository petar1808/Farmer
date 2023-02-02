namespace Application.Features.Identity.Commands.Login
{
    public class LoginOutputCommandModel
    {
        public LoginOutputCommandModel(string token)
        {
            Token = token;
        }

        public string Token { get; set; } = default!;
    }
}
