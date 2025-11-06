using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace OnlineLearningMVC.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _cfg;
        public SmtpEmailSender(IConfiguration cfg) { _cfg = cfg; }

        public Task SendEmailAsync(string to, string subject, string htmlMessage)
        {
            var smtp = new SmtpClient(_cfg[""Smtp:Host""])
            {
                Port = int.Parse(_cfg[""Smtp:Port""] ?? ""25""),
                EnableSsl = bool.Parse(_cfg[""Smtp:EnableSsl""] ?? ""false"")
            };
            var mail = new MailMessage(_cfg[""Smtp:From""], to, subject, htmlMessage) { IsBodyHtml = true };
            smtp.Send(mail);
            return Task.CompletedTask;
        }
    }
}
