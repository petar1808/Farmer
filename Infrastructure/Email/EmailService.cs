using Application.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _smtpSettings;
        private readonly ILogger<EmailService> _logger;
        private const string newLine = "<br>";
        public EmailService(IOptions<EmailSettings> _smtpSettings, ILogger<EmailService> logger)
        {
            this._smtpSettings = _smtpSettings.Value;
            this._logger = logger;
        }

        private MimeMessage CreateMessage(MailboxAddress receiver, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.Server, _smtpSettings.UserName));
            message.To.Add(receiver);
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html) { Text = body };
            return message;
        }

        private async Task<bool> SendAsync(MimeMessage message)
        {
            try
            {
                using (var emailClient = new SmtpClient())
                {
                    await emailClient.ConnectAsync(this._smtpSettings.Server, this._smtpSettings.Port);

                    await emailClient.AuthenticateAsync(this._smtpSettings.UserName, this._smtpSettings.Password);

                    await emailClient.SendAsync(message);

                    await emailClient.DisconnectAsync(true);
                }

                _logger.LogInformation("Email sent successfully to {Recipient}.", message.To);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending email to {Recipient}.", message.To);
                return false;
            }
        }

        public async Task<bool> SendUserCreatedEmail(string userName, string userEmail, string url)
        {
            string body = $"Здравейте, <b>{userName}</b>," + newLine +
                          newLine +
                          $"Вие бяхте добавен(а) в платформата на <b>Фермер</b>. За да активирате своя акаунт, моля, натиснете <a href='{url}'>тук.</a>" + newLine;

            var message = CreateMessage(new MailboxAddress(userName, userEmail), "Регистрация в Фермер", body);

            return await this.SendAsync(message);
        }

        public async Task SendResetPasswordEmail(string userName, string userEmail, string url)
        {
            string body = $"Здравейте <b>{userName}</b>," + newLine +
                          newLine +
                          $"Забравили сте Вашата парола за платформата на <b>Фермер</b>? " + newLine +
                          $"За да смените паролата си, моля, натиснете <a href='{url}'>тук.</a>" + newLine;

            var message = CreateMessage(new MailboxAddress(userName, userEmail), "Смяна на парола в Фермер", body);

            await this.SendAsync(message);
        }
    }
}
