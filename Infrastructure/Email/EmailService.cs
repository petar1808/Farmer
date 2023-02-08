using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Microsoft.Extensions.Logging;
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

        private async Task<bool> SendAsync(MailboxAddress receiver, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();

                message.To.Add(receiver);

                message.From.Add(
                    new MailboxAddress(
                        this._smtpSettings.Server,
                        this._smtpSettings.UserName));

                message.Subject = subject;

                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = body
                };

                using (var emailClient = new SmtpClient())
                {
                    await emailClient.ConnectAsync(this._smtpSettings.Server, this._smtpSettings.Port);

                    await emailClient.AuthenticateAsync(this._smtpSettings.UserName, this._smtpSettings.Password);

                    await emailClient.SendAsync(message);

                    await emailClient.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> SendUserCreatedEmail(string userName, string userEmail, string url)
        {
            string body = $"Здравейте, <b>{userName}</b>," + newLine +
                          newLine +
                          $"Вие бяхте добавен(а) в платформата на <b>Фермер</b>. За да активирате своя акаунт, моля, натиснете <a href='{url}'>тук.</a>" + newLine ;

            return await this.SendAsync(new MailboxAddress(userName, userEmail), "Регистрация в Фермер", body);
        }

        public async Task SendResetPasswordEmail(string userName, string userEmail, string url)
        {
            string body = $"Здравейте <b>{userName}</b>," + newLine +
                          newLine +
                          $"Забравили сте Вашата парола за платформата на <b>Фермер</b>? " + newLine +
                          $"За да смените паролата си, моля, натиснете <a href='{url}'>тук.</a>" + newLine;

            await this.SendAsync(new MailboxAddress(userName, userEmail), "Смяна на парола в Фермер", body);
        }
    }
}
