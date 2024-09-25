using MailKit.Net.Smtp;
using MimeKit;

namespace SendingAnEmailSMTP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("name", "email@gmail.com"));
            message.To.Add(new MailboxAddress("name", "email@gmail.com"));
            message.Subject = "test?";

            message.Body = new TextPart("plain")
            {
                Text = @"test emaillll
                        "
            };

            using (var client = new SmtpClient())
            {
                // Conectar al servidor SMTP de Gmail
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // Autenticarse
                client.Authenticate("email@gmail.com", "password");

                // Enviar el correo
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
