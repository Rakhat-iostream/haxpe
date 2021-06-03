namespace Haxpe
{
    public static class HaxpeDomainErrorCodes
    {
        public const string AccountInvalidUserNameOrPassword = "accountInvalidUserNameOrPassword";
        public const string AccountNotAllowed = "accountNotAllowed";
        public const string AccountLockedOut = "accountLockedOut";
        public const string AccountRequiresTwoFactor = "accountRequiresTwoFactor";
        public const string AccountExternalUserPasswordChange = "accountExternalUserPasswordChange";
        public const string AccountInvalidEmailAddress = "accountInvalidEmailAddress";
        public const string AccountFacebookNoEmail = "accountFacebookNoEmail";
        public const string AccountGoogleNoEmail = "accountGoogleNoEmail";

        public const string CustomerNotFound = "customerNotFound"; 
        public const string OrderNotFound = "orderNotFound";
        public const string CouponNotFound = "couponNotFound";

        public const string OrderAssignWorkerFromOtherPartner = "orderAssignWorkerFromOtherPartner";
        public const string OrderWorkflowViolation = "orderWorkflowViolation";

        public const string TooManyObjectsToReturn = "tooManyObjectsToReturn";

        public const string NotFound = "notFound";
    }
}
