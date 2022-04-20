using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;
using Application.Services;

namespace Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            this.emailSettings = options.Value;
        }

        public async Task SendUserChangePassword(string userEmail, string password)
        {
            var subject = $"Вашата парола беше сменена";
            var text = $"Вашият емайл е: {userEmail} - Вашата нова парола e: {password}";

            await SendUserEmail(userEmail, password, subject, text);
        }

        public async Task SendUserCreatedEmail(string userEmail, string password)
        {
            var subject = $"Вие бяхте успешно регистриран в Фермер";
            var text = $"Вашият емайл е: {userEmail} - Вашата парола e: {password}";

            await SendUserEmail(userEmail, password,subject, text);
        }

        private async Task SendUserEmail(string userEmail, string password, string subject , string text)
        {
            var message = new MimeMessage();

            message.To.Add(new MailboxAddress(userEmail, userEmail));

            message.From.Add(new MailboxAddress(this.emailSettings.UserName, this.emailSettings.UserName));

            // 
            message.Subject = subject;

            message.Body = new TextPart
            {
                Text = text
            };

            using (var emailClient = new SmtpClient())
            {
                await emailClient.ConnectAsync(emailSettings.Server, emailSettings.Port);

                await emailClient.AuthenticateAsync(emailSettings.UserName, emailSettings.Password);

                await emailClient.SendAsync(message);

                await emailClient.DisconnectAsync(true);
            }
        }
    }
}
