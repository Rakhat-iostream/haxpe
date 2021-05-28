using System;
using System.Threading.Tasks;

namespace Haxpe.V1.Emails
{
    public interface IEmailService
    {
        Task SendPartnerRegistrationConfirm(string emailAddress, string language, PartnerRegistrationConfirmModel model);

        Task SendPartnerWelcome(string emailAddress, string language, PartnerWelcomeModel model);

        Task SendPartnerVerificationFailed(string emailAddress, string language, PartnerVerificationFailedModel model);

        Task SendWorkerRegistrationConfirm(string emailAddress, string language, WorkerRegistrationConfirmModel model);

        Task SendCustomerRegistrationConfirm(string emailAddress, string language, CustomerRegistrationConfirmModel model);

        Task SendCustomerWelcome(string emailAddress, string language, CustomerWelcomeModel model);

        Task SendThanksForOrder(string emailAddress, string language, ThanksForOrderModel model);

        Task SendPasswordResetLink(string emailAddress, string language, ResetPasswordModel model);
    }
}
