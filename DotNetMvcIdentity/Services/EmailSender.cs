using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using System.Configuration;

namespace DotNetMvcIdentity.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration) {
            _configuration = configuration;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Support identity app", _configuration["SMTP:user"]));
            message.To.Add(new MailboxAddress("Dear user", email));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = htmlMessage
            };

            using (var client = new SmtpClient())
            {
                // Conectar al servidor SMTP de Gmail
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // Autenticarse
                client.Authenticate(_configuration["SMTP:user"], _configuration["SMTP:password"]);

                // Enviar el correo
                try
                {
                    client.Send(message);
                    client.Disconnect(true);
                }
                catch (Exception ex)
                {
                    return Task.FromException(ex);
                }
            }
            return Task.CompletedTask;
        }
    }
}
