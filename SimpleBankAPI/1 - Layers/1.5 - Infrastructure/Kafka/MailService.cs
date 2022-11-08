using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SimpleBankAPI.Infrastructure.Kafka
{
    public class MailService : ICommunicationsService
        {
            private readonly IConfiguration _configuration;
            public MailService(IConfiguration config)
            {
                _configuration = config;
            }
            public async Task SendCommunication(Communication communication)
            {
                //Build Message 
                var ToEmail = communication.User.Email;
                var Subject = communication.Subject;
                var Body = communication.Body;
 
                //build email configuration
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_configuration["MailSettings:Mail"]);
                email.To.Add(MailboxAddress.Parse(ToEmail));
                email.Subject = Subject;
                var builder = new BodyBuilder();
           
                builder.HtmlBody = Body;
                email.Body = builder.ToMessageBody();

                //MailSettings
                using var smtp = new SmtpClient();
                smtp.Connect(_configuration["MailSettings:Host"]
                    ,int.Parse(_configuration["MailSettings:Port"])
                    , SecureSocketOptions.StartTls);
                smtp.Authenticate(_configuration["MailSettings:Mail"]
                    , _configuration["MailSettings:Password"]);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
        }
    }
}
