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
        public const string AccountInvalidEmail = "accountInvalidEmail";
        public const string AccountDuplicateEmail = "accountDuplicateEmail";
        public const string AccountDefaultError = "accountDefaultError";
        public const string AccountPasswordMismatch = "accountPasswordMismatch";    
        public const string AccountPasswordTooShort = "accountPasswordTooShort";
        public const string AccountPasswordRequiresUniqueChars = "accountPasswordRequiresUniqueChars";
        public const string AccountPasswordRequiresNonAlphanumeric = "accountPasswordRequiresNonAlphanumeric";
        public const string AccountPasswordRequiresDigit = "accountPasswordRequiresDigit";
        public const string AccountPasswordRequiresLower = "accountPasswordRequiresLower";
        public const string AccountPasswordRequiresUpper = "accountPasswordRequiresUpper";
        public const string AccountUserLockoutNotEnabled = "accountUserLockoutNotEnabled";
        public const string AccountInvalidToken = "accountInvalidToken";
        public const string AccountDuplicateUserName = "accountDuplicateUserName";
        public const string AccountFailedOperation = "accountFailedOperation";
        public const string AccountLoginAlreadyAssociated = "accountLoginAlreadyAssociated";

        public const string UserNotFound = "UserNotFound";
        public const string CustomerNotFound = "customerNotFound"; 
        public const string OrderNotFound = "orderNotFound";
        public const string CouponNotFound = "couponNotFound";

        public const string OrderAssignWorkerFromOtherPartner = "orderAssignWorkerFromOtherPartner";
        public const string OrderWorkflowViolation = "orderWorkflowViolation";

        public const string TooManyObjectsToReturn = "tooManyObjectsToReturn";

        public const string NotFound = "notFound";
        public const string WorkerLocationNotFound = "workerLocationNotFound";
    }
}
