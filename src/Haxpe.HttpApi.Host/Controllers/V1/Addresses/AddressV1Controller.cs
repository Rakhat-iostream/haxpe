using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Haxpe.Addresses;
using Haxpe.Infrastructure;
using Haxpe.Models;
using Haxpe.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Haxpe.V1.Addresses
{
    [ApiVersion("1.0")]
    [ApiController]
    [ControllerName("Address")]
    [Authorize]
    public class AddressV1Controller : ControllerBase
    {
        private readonly IAddressV1Service service;

        public AddressV1Controller(IAddressV1Service service)
        {
            this.service = service;
        }

        [Route("api/v1/address/{id}")]
        [HttpGet]
        public async Task<Response<AddressV1Dto>> GetAsync(Guid id)
        {
            var res = await service.FindAsync(id);
            return Response<AddressV1Dto>.Ok(res);
        }

        [Route("api/v1/address/page")]
        [HttpGet]
        public async Task<Response<PagedResultDto<AddressV1Dto>>> GetPageAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await service.GetPageAsync(input);
            return Response<Response<PagedResultDto<AddressV1Dto>>>.Ok(res);
        }

        [Route("api/v1/address")]
        [HttpGet]
        public async Task<Response<IReadOnlyCollection<AddressV1Dto>>> GetListAsync([FromQuery] AddressListQuery input)
        {
            var res = await service.GetListAsync(input);
            return Response<Response<IReadOnlyCollection<AddressV1Dto>>>.Ok(res);
        }

        [Route("api/v1/address")]
        [HttpPost]
        public async Task<Response<AddressV1Dto>> CreateAsync([FromBody] UpdateAddressV1Dto input)
        {
            var res =  await service.CreateAsync(input);
            return Response<AddressV1Dto>.Ok(res);
        }

        [Route("api/v1/address/{id}")]
        [HttpDelete]
        [Authorize(Roles=RoleConstants.Admin)]
        public async Task<Response> DeleteAsync(Guid id)
        {
            await service.DeleteAsync(id);
            return Haxpe.Models.Response.Ok();
        }
    }
}   