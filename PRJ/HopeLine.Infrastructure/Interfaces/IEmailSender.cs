using System.Threading.Tasks;

namespace HopeLine.Infrastructure.Interfaces
{
    interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}