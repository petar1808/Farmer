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
            var body = $@"<div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;'>
                                <h2 style='color: #4CAF50; font-weight: bold; text-align: center;'>Добре дошли в <b>Фермер</b>, {userName}!</h2>

                                <p style='font-size: 16px; line-height: 1.6;'>
                                    Вие бяхте добавен(а) в платформата <b>Фермер</b>. За да активирате своя акаунт и да започнете да използвате услугите, моля, натиснете бутона по-долу.
                                </p>
            
                                <div style='text-align: center; margin: 20px 0;'>
                                    <a href='{url}' style='background-color: #4CAF50; color: white; padding: 12px 24px; font-size: 16px; font-weight: bold; text-decoration: none; border-radius: 5px;'>
                                        Активирайте акаунта
                                    </a>
                                </div>
            
                                <p style='font-size: 14px; color: #888; text-align: center;'>
                                    Ако не сте искали създаване на акаунт, моля, игнорирайте това съобщение.
                                </p>
            
                                <p style='font-size: 14px; color: #888; text-align: center; margin-top: 20px;'>
                                    С най-добри пожелания,<br>
                                    Екипът на <b>Фермер</b>
                                </p>
                        </div>";

            var message = CreateMessage(new MailboxAddress(userName, userEmail), "Регистрация в Фермер", body);

            return await this.SendAsync(message);
        }

        public async Task SendResetPasswordEmail(string userName, string userEmail, string url)
        {
            var body = $@"<div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;'>
                            <h2 style='color: #FF5733; font-weight: bold; text-align: center;'>Възстановяване на паролата за {userName}</h2>

                            <p style='font-size: 16px; line-height: 1.6;'>
                                Изглежда, че сте забравили паролата си за платформата <b>Фермер</b>. За да я възстановите, натиснете бутона по-долу.
                            </p>
            
                            <div style='text-align: center; margin: 20px 0;'>
                                <a href='{url}' style='background-color: #FF5733; color: white; padding: 12px 24px; font-size: 16px; font-weight: bold; text-decoration: none; border-radius: 5px;'>
                                    Възстановете паролата
                                </a>
                            </div>
            
                            <p style='font-size: 14px; color: #888; text-align: center;'>
                                Ако не сте поискали смяна на паролата, моля, игнорирайте това съобщение.
                            </p>
            
                            <p style='font-size: 14px; color: #888; text-align: center; margin-top: 20px;'>
                                С най-добри пожелания,<br>
                                Екипът на <b>Фермер</b>
                            </p>
                        </div>";

            var message = CreateMessage(new MailboxAddress(userName, userEmail), "Смяна на парола в Фермер", body);

            await this.SendAsync(message);
        }
    }
}
