using System;
using System.Linq;
using System.Threading.Tasks;
using Haxpe.Infrastructure;
using Haxpe.Models;
using Haxpe.Roles;
using Haxpe.Users;
using Haxpe.V1.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Haxpe.V1.Partners
{
    [ApiVersion("1.0")]
    [ApiController]
    [ControllerName("Partner")]
    [Authorize]
    public class PartnerV1Controller : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IPartnerV1Service partnerV1Service;

        public PartnerV1Controller(
            UserManager<User> userManager,
            IPartnerV1Service partnerV1Service
        ) 
        {
            this.userManager = userManager;
            this.partnerV1Service = partnerV1Service;
        }


        [Route("api/v1/partner/{id}")]
        [HttpGet]
        public async Task<Response<PartnerV1Dto>> GetAsync(Guid id)
        {
            var res = await partnerV1Service.FindAsync(id);
            return Response<PartnerV1Dto>.Ok(res);
        }

        [Route("api/v1/partner/page")]
        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<Response<PagedResultDto<PartnerV1Dto>>> GetPageAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await partnerV1Service.GetPageAsync(input);
            return Response<Response<PagedResultDto<PartnerV1Dto>>>.Ok(res);
        }

        [Route("api/v1/partner")]
        [HttpGet]
        public async Task<Response<PagedResultDto<PartnerV1Dto>>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await partnerV1Service.GetPageAsync(input);
            return Response<PartnerV1Dto>.Ok(res);
        }

        [Route("api/v1/partner")]
        [HttpPost]
        public async Task<Response<PartnerV1Dto>> CreateAsync([FromBody] UpdatePartnerV1Dto input)
        {
            var res = await partnerV1Service.CreateAsync(input);
            return Response<PartnerV1Dto>.Ok(res);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [Route("api/v1/partner/create-full")]
        [HttpPost]
        public async Task<Response<PartnerV1Dto>> CreateFullAsync([FromBody] CreatePartnerV1Dto input)
        {
            var partnerId = Guid.NewGuid();

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = input.OwnerEmail,
                Email = input.OwnerEmail,
                Name = input.OwnerFirstName,
                Surname = input.OwnerLastName,
                PhoneNumber = input.OwnerPhone,
                PartnerId = partnerId
            };
            user.SetFullName(user.Name, user.Surname);

            var res = await userManager.CreateAsync(user, "Pass!123");
            res.CheckErrors();

            await userManager.SetEmailAsync(user, input.OwnerEmail);
            await userManager.AddToRoleAsync(user, RoleConstants.Partner);

            var partner = await partnerV1Service.CreateAsync(new UpdatePartnerV1Dto {
                Id = partnerId,
                Name = input.Name,
                OwnerId = user.Id,
                Description = input.Description,
                AddressId = input.AddressId }
            );

            return Response<PartnerV1Dto>.Ok(partner);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [Authorize(Roles = RoleConstants.Partner)]
        [Route("api/v1/partner/{id}")]
        [HttpPut]
        public  async Task<Response<PartnerV1Dto>> UpdateAsync(Guid id, [FromBody] UpdatePartnerV1Dto input)
        {
            var res = await partnerV1Service.UpdateAsync(id, input);
            return Response<PartnerV1Dto>.Ok(res);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [Route("api/v1/partner/{id}")]
        [HttpDelete]
        public async Task<Response> DeleteAsync(Guid id)
        {
            await partnerV1Service.DeleteAsync(id);
            return Haxpe.Models.Response.Ok();
        }
    }
}