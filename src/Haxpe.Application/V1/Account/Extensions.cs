using Haxpe.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Account
{
    public static class Extensions
    {
        public static void CheckErrors(this IdentityResult result)
        {
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    switch (error.Code)
                    {
                        case "DefaultError":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountDefaultError);
                        case "LoginAlreadyAssociated":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountLoginAlreadyAssociated);
                        case "InvalidEmail":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountInvalidEmail);
                        case "DuplicateEmail":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountDuplicateEmail);
                        case "InvalidUserName":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountInvalidUserNameOrPassword);
                        case "DuplicateUserName":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountDuplicateUserName);
                        case "PasswordMismatch":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountPasswordMismatch);
                        case "PasswordTooShort":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountPasswordTooShort);
                        case "PasswordRequiresUniqueChars":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountPasswordRequiresUniqueChars);
                        case "PasswordRequiresNonAlphanumeric":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountPasswordRequiresNonAlphanumeric);
                        case "PasswordRequiresDigit":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountPasswordRequiresDigit);
                        case "PasswordRequiresLower":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountPasswordRequiresLower);
                        case "PasswordRequiresUpper":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountPasswordRequiresUpper);
                        case "UserLockoutNotEnabled":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountUserLockoutNotEnabled);
                        case "InvalidToken":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountInvalidToken);
                        case "FailedOperation":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountFailedOperation);
                        default:
                            Log.Logger.Error($"Internal error code = {error.Code}");
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountDefaultError);
                    }
                }
            }
        }
    }
}