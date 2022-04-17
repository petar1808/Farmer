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


        // То userName, UserEmail, Password
        public async Task SendUserCreatedEmail()
        {
            var message = new MimeMessage();

            message.To.Add(new MailboxAddress("Petar", "beroicite@abv.bg"));

            message.From.Add(new MailboxAddress(this.emailSettings.UserName, this.emailSettings.UserName));

            // 
            message.Subject = "Nov";

            message.Body = new TextPart
            {
                Text = "gotovo"
            };

            using(var emailClient = new SmtpClient())
            {
                await emailClient.ConnectAsync(emailSettings.Server, emailSettings.Port);

                await emailClient.AuthenticateAsync(emailSettings.UserName, emailSettings.Password);

                await emailClient.SendAsync(message);

                await emailClient.DisconnectAsync(true);
            }
        }
    }
}
