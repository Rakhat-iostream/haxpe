using Haxpe.Infrastructure;
using Microsoft.AspNetCore.Identity;
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
                        case "InvalidEmail":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountInvalidEmail);
                        case "DuplicateEmail":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountDuplicateEmail);
                        case "PasswordTooShort":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountPasswordTooShort);
                        case "PasswordRequiresNonAlphanumeric":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountPasswordRequiresNonAlphanumeric);
                        case "PasswordRequiresDigit":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountPasswordRequiresDigit);
                        case "UserLockoutNotEnabled":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountUserLockoutNotEnabled);
                        case "InvalidToken":
                            throw new BusinessException(HaxpeDomainErrorCodes.AccountInvalidToken);
                    }
                }
            }
        }
    }
}