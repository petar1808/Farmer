namespace Application.Services
{
    public interface IEmailService
    {
        Task<bool> SendUserCreatedEmail(string userName, string userEmail, string url);

        Task SendResetPasswordEmail(string userName, string userEmail, string url);
    }
}
