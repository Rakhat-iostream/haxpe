using System;
using System.Threading.Tasks;
using Haxpe.Infrastructure;
using Haxpe.V1.Common;
using Haxpe.V1.Facebook;
using Haxpe.V1.Google;

namespace Haxpe.V1.Account
{
    public interface IAccountAppService : IApplicationService
    {
        Task<UserProfileDto> RegisterAsync(RegisterDto input);

        Task ConfirmEmailAsync(ConfirmEmailDto input);

        Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input);

        Task ResetPasswordAsync(ResetPasswordDto input);

        Task<UserProfileDto> Login(UserLoginInfo login);

        Task Logout();

        Task<UserProfileDto> GetProfileAsync();

        Task<UserProfileDto> UpdateProfileAsync(UpdateUserProfileDto input);

        Task ChangePasswordAsync(ChangePasswordInput input);

        Task<UserProfileDto> FacebookLogin(FacebookCredentials credentials);

        Task<UserProfileDto> GoogleLogin(GoogleCredentials credentials);
    }
}