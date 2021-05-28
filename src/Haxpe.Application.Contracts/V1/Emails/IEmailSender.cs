using System.Threading.Tasks;

namespace Haxpe.V1.Emails
{
    public interface IEmailSender
    {
        Task SendAsync(string address, string subject, string body);
    }
}