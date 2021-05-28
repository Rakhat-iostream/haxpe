using AutoMapper;
using Haxpe.Users;
using Haxpe.V1.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Haxpe.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public CurrentUserService(UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var user = await this.userManager.GetUserAsync(this.httpContextAccessor.HttpContext.User);
            return user;
        }

        public Task<Guid> GetCurrentUserIdAsync()
        {
            var id = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Task.FromResult(Guid.Parse(id));
        }

        public async Task<IReadOnlyCollection<string>> GetCurrentUserRolesAsync()
        {
            var user = await this.GetCurrentUserAsync();
            var roles = await this.userManager.GetRolesAsync(user);
            return roles.ToArray();
        }

        public async Task<IReadOnlyCollection<string>> GetCurrentUserRolesAsync(User user)
        {
            var roles = await this.userManager.GetRolesAsync(user);
            return roles.ToArray();
        }
    }
}
