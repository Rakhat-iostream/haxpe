using System;
using System.Threading.Tasks;
using Haxpe.V1.Partners;
using Haxpe.Customers;
using Haxpe.Partners;
using Haxpe.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Haxpe.Infrastructure;
using Haxpe.Users;
using Haxpe.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Haxpe.V1.Account;

namespace Haxpe.V1.Customers
{
    [ApiVersion("1.0")]
    [ApiController]
    [ControllerName("Customer")]
    [Authorize]
    public class CustomerV1Controller : ControllerBase
    {
        private readonly ICustomerV1Service service;
        private readonly ICurrentUserService currentUserService;
        private readonly UserManager<User> userManager;
        protected SignInManager<User> signInManager;

        public CustomerV1Controller(
            ICustomerV1Service service, 
            ICurrentUserService currentUserService,
            UserManager<User> userManager,
            SignInManager<User> signInManager
        )
        {
            this.service = service;
            this.currentUserService = currentUserService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Route("api/v1/customer/{id}")]
        [HttpGet]
        public async Task<Response<CustomerV1Dto>> GetAsync(Guid id)
        {
            var res = await service.FindAsync(id);
            await Check(res, RoleConstants.Admin, RoleConstants.Partner, RoleConstants.Worker);
            return Response<CustomerV1Dto>.Ok(res);
        }

        [Route("api/v1/customer/info")]
        [HttpGet]
        public async Task<Response<CustomerV1Dto>> GetInfoAsync()
        {
            var res = await service.GetByUserId(await this.currentUserService.GetCurrentUserIdAsync());
            return Response<CustomerV1Dto>.Ok(res);
        }

        [Route("api/v1/customer/page")]
        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<Response<PagedResultDto<CustomerV1Dto>>> GetPageAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await service.GetPageAsync(input);
            return Response<Response<PagedResultDto<CustomerV1Dto>>>.Ok(res);
        }

        [Route("api/v1/customer")]
        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<Response<IReadOnlyCollection<CustomerV1Dto>>> GetListAsync([FromQuery] CustomerListQuery input)
        {
            var res = await service.GetListAsync(input);
            return Response<Response<IReadOnlyCollection<CustomerV1Dto>>>.Ok(res);
        }

        [Route("api/v1/customer")]
        [HttpPost]
        public async Task<Response<CustomerV1Dto>> CreateAsync([FromBody] UpdateCustomerV1Dto input)
        {
            var res = await service.CreateAsync(input);
            var user = await this.userManager.FindByIdAsync(res.UserId.ToString());
            (await this.userManager.AddToRoleAsync(user, RoleConstants.Customer)).CheckErrors();
            await this.signInManager.SignInAsync(user, true);
            return Response<CustomerV1Dto>.Ok(res);
        }

        [Route("api/v1/customer/{id}")]
        [HttpPut]
        public async Task<Response<CustomerV1Dto>> UpdateAsync(Guid id, [FromBody] UpdateCustomerV1Dto input)
        {
            await Check(await this.service.FindAsync(id), RoleConstants.Admin);
            var res = await service.UpdateAsync(id, input);
            return Response<CustomerV1Dto>.Ok(res);
        }

        private async Task Check(CustomerV1Dto customer, params string[] roles)
        {
            var currentUserId = await this.currentUserService.GetCurrentUserIdAsync();
            var userRoles = await this.currentUserService.GetCurrentUserRolesAsync();

            if (!(
                    roles.Intersect(userRoles).Any() ||
                    customer.UserId == currentUserId
                )
            )
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}