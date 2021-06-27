using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Haxpe.Models;
using Haxpe.Users;
using Haxpe.V1.Common;
using Haxpe.V1.Events;
using Haxpe.V1.Facebook;
using Haxpe.V1.Google;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Haxpe.V1.Account
{
    [ApiVersion("1.0")]
    [ApiController]
    [ControllerName("Account")]
    public class AccountV1Controller : ControllerBase
    {
        private readonly IAccountAppService service;
        private readonly ICurrentUserService currentUserService;
        private readonly IJwtEncoder encoder;
        private readonly CentrifugoSettings settings;

        public AccountV1Controller(IAccountAppService service, ICurrentUserService currentUserService, CentrifugoSettings settings)
        {
            this.service = service;
            this.currentUserService = currentUserService;
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            this.encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            this.settings = settings;
        }

        [HttpPost]
        [Route("api/v1/account/register")]
        public async Task<Response<UserProfileDto>> RegisterAsync([FromBody] RegisterDto input)
        {
            var res = await service.RegisterAsync(input);
            return Response<UserProfileDto>.Ok(res);
        }

        [HttpPost]
        [Route("api/v1/account/confirm-email")]
        public async Task<Response> ConfirmEmailAsync([FromBody] ConfirmEmailDto input)
        {
            await service.ConfirmEmailAsync(input);
            return Haxpe.Models.Response.Ok();
        }

        [HttpPost]
        [Route("api/v1/account/send-password-reset-code")]
        public Task SendPasswordResetCodeAsync([FromBody] SendPasswordResetCodeDto input)
        {
            return service.SendPasswordResetCodeAsync(input);
        }

        [HttpPost]
        [Route("api/v1/account/reset-password")]
        public Task ResetPasswordAsync([FromBody] ResetPasswordDto input)
        {
            return service.ResetPasswordAsync(input);
        }

        [HttpPost]
        [Route("api/v1/account/login")]
        public async Task<Response<UserProfileDto>> Login([FromBody] UserLoginInfo login)
        {
            var res = await service.Login(login);
            return Response<UserProfileDto>.Ok(res);
        }

        [HttpPost]
        [Route("api/v1/account/logout")]
        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        }

        [HttpGet]
        [Route("api/v1/account/profile")]
        [Authorize]
        public async Task<Response<UserProfileDto>> GetProfileAsync()
        {
            var res = await service.GetProfileAsync();
            return Response<UserProfileDto>.Ok(res);
        }

        [HttpPut]
        [Route("api/v1/account/profile")]
        [Authorize]
        public async Task<Response<UserProfileDto>> UpdateProfileAsync([FromBody] UpdateUserProfileDto input)
        {
            var res = await service.UpdateProfileAsync(input);
            return Response<UserProfileDto>.Ok(res);
        }

        [HttpPost]
        [Route("api/v1/account/change-password")]
        [Authorize]
        public Task ChangePasswordAsync([FromBody] ChangePasswordInput input)
        {
            return service.ChangePasswordAsync(input);
        }

        [HttpPost]
        [Route("api/v1/account/login-facebook")]
        public async Task<Response<UserProfileDto>> FacebookLogin([FromBody] FacebookCredentials credentials)
        {
            var res = await service.FacebookLogin(credentials);
            return Response<UserProfileDto>.Ok(res);
        }

        [HttpPost]
        [Route("api/v1/account/login-google")]
        public async Task<Response<UserProfileDto>> GoogleLogin([FromBody] GoogleCredentials credentials)
        {
            var res = await service.GoogleLogin(credentials);
            return Response<UserProfileDto>.Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("api/v1/account/websocket-token")]
        public async Task<Response<string>> GetWebsocketToken()
        {
            var payload = new Dictionary<string, object>
            {
                { "sub", await this.currentUserService.GetCurrentUserIdAsync() },
            };
            var token = this.encoder.Encode(payload, this.settings.Secret);
            return Response<string>.Ok(token);
        }
    }
}
