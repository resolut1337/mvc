using System.Threading.Tasks;

namespace OnlineLearningMVC.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string htmlMessage);
    }
}
