using System;
using System.Threading.Tasks;
using Application.Services;
using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;
        private readonly EmailClient _emailClient;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
            _emailClient = new EmailClient(_emailSettings.ConnectionString);
        }

        public async Task<bool> SendUserCreatedEmail(string userName, string userEmail, string url)
        {
            string subject = "Регистрация в Фермер";
            string body = GenerateWelcomeEmailBody(userName, url);

            return await SendEmailAsync(userEmail, subject, body);
        }

        public async Task SendResetPasswordEmail(string userName, string userEmail, string url)
        {
            string subject = "Смяна на парола в Фермер";
            string body = GenerateResetPasswordEmailBody(userName, url);

            await SendEmailAsync(userEmail, subject, body);
        }

        private async Task<bool> SendEmailAsync(string recipientEmail, string subject, string htmlBody)
        {
            try
            {
                var emailMessage = new EmailMessage(_emailSettings.SenderEmail, recipientEmail, new EmailContent(subject)
                {
                    Html = htmlBody
                });

                EmailSendOperation emailSendOperation = await _emailClient.SendAsync(WaitUntil.Completed, emailMessage);

                _logger.LogInformation("Email sent successfully to {Recipient}. Email ID: {EmailId}", recipientEmail, emailSendOperation.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending email to {Recipient}.", recipientEmail);
                return false;
            }
        }

        private string GenerateWelcomeEmailBody(string userName, string url)
        {
            return $@"<div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;'>
                        <h2 style='color: #4CAF50; font-weight: bold; text-align: center;'>Добре дошли в <b>Фермер</b>, {userName}!</h2>
                        <p style='font-size: 16px; line-height: 1.6;'>Вие бяхте добавен(а) в платформата <b>Фермер</b>. За да активирате своя акаунт и да започнете да използвате услугите, моля, натиснете бутона по-долу.</p>
                        <div style='text-align: center; margin: 20px 0;'>
                            <a href='{url}' style='background-color: #4CAF50; color: white; padding: 12px 24px; font-size: 16px; font-weight: bold; text-decoration: none; border-radius: 5px;'>Активирайте акаунта</a>
                        </div>
                        <p style='font-size: 14px; color: #888; text-align: center;'>Ако не сте искали създаване на акаунт, моля, игнорирайте това съобщение.</p>
                        <p style='font-size: 14px; color: #888; text-align: center; margin-top: 20px;'>С най-добри пожелания,<br>Екипът на <b>Фермер</b></p>
                    </div>";
        }

        private string GenerateResetPasswordEmailBody(string userName, string url)
        {
            return $@"<div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;'>
                        <h2 style='color: #FF5733; font-weight: bold; text-align: center;'>Възстановяване на паролата за {userName}</h2>
                        <p style='font-size: 16px; line-height: 1.6;'>Изглежда, че сте забравили паролата си за платформата <b>Фермер</b>. За да я възстановите, натиснете бутона по-долу.</p>
                        <div style='text-align: center; margin: 20px 0;'>
                            <a href='{url}' style='background-color: #FF5733; color: white; padding: 12px 24px; font-size: 16px; font-weight: bold; text-decoration: none; border-radius: 5px;'>Възстановете паролата</a>
                        </div>
                        <p style='font-size: 14px; color: #888; text-align: center;'>Ако не сте поискали смяна на паролата, моля, игнорирайте това съобщение.</p>
                        <p style='font-size: 14px; color: #888; text-align: center; margin-top: 20px;'>С най-добри пожелания,<br>Екипът на <b>Фермер</b></p>
                    </div>";
        }
    }
}
