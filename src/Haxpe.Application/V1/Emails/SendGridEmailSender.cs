using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Haxpe.V1.Emails
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly SendGridClient client;

        public SendGridEmailSender(SendGridClient client)
        {
            this.client = client;
        }

        public async Task SendAsync(string address, string subject, string body)
        {
            var message = MailHelper.CreateSingleEmail(
                new EmailAddress("noreply@haxpe.com", "Haxpe"),
                new EmailAddress(address),
                subject,
                null,
                body
            );

            await this.client.SendEmailAsync(message);
        }

        public async Task SendEmailAsync(MailMessage mail)
        {
            var message = MailHelper.CreateSingleEmailToMultipleRecipients(
                new EmailAddress(mail.From.Address, mail.From.DisplayName),
                mail.To.Select(x => new EmailAddress(x.Address, x.DisplayName)).ToList(),
                mail.Subject,
                mail.IsBodyHtml ? null : mail.Body,
                mail.IsBodyHtml ? mail.Body : null);

            await this.client.SendEmailAsync(message);
        }
    }
}
