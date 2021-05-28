using System;
using System.Threading.Tasks;

namespace Haxpe.V1.Emails
{
    public class EmailService: IEmailService
    {
        private readonly IEmailSender emailSender;
        private readonly ITemplateRenderer templateRenderer;

        public EmailService(
            IEmailSender emailSender,
            ITemplateRenderer templateRenderer)
        {
            this.emailSender = emailSender;
            this.templateRenderer = templateRenderer;
        }

        public Task SendCustomerRegistrationConfirm(string emailAddress, string language, CustomerRegistrationConfirmModel model)
        {
            return Send(emailAddress, language, "Haxpe", model);
        }

        public Task SendCustomerWelcome(string emailAddress, string language, CustomerWelcomeModel model)
        {
            return Send(emailAddress, language, "Haxpe", model);
        }

        public Task SendPartnerRegistrationConfirm(string emailAddress, string language, PartnerRegistrationConfirmModel model)
        {
            return Send(emailAddress, language, "Haxpe", model);
        }

        public Task SendPartnerVerificationFailed(string emailAddress, string language, PartnerVerificationFailedModel model)
        {
            return Send(emailAddress, language, "Haxpe", model);
        }

        public Task SendPartnerWelcome(string emailAddress, string language, PartnerWelcomeModel model)
        {
            return Send(emailAddress, language, "Haxpe", model);
        }

        public Task SendPasswordResetLink(string emailAddress, string language, ResetPasswordModel model)
        {
            return Send(emailAddress, language, "Haxpe", model);
        }

        public Task SendThanksForOrder(string emailAddress, string language, ThanksForOrderModel model)
        {
            return Send(emailAddress, language, "Haxpe", model);
        }

        public Task SendWorkerRegistrationConfirm(string emailAddress, string language, WorkerRegistrationConfirmModel model)
        {
            return Send(emailAddress, language, "Haxpe", model);
        }

        private async Task Send<T>(string address, string language, string subject, T model)
        {
            var body = await this.templateRenderer.RenderAsync(
                typeof(T).Name.Replace("Model", ""),
                model,
                cultureName: language
            );

            await this.emailSender.SendAsync(
                address,
                subject,
                body
            );
        }
    }
}
