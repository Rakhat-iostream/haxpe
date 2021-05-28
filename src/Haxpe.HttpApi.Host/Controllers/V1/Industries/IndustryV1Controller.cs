using System;
using System.Linq;
using System.Threading.Tasks;
using Haxpe.Infrastructure;
using Haxpe.Models;
using Haxpe.Roles;
using Haxpe.V1.Industry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Haxpe.V1.Industries
{
    [ApiVersion("1.0")]
    [ApiController]
    [ControllerName("Industry")]
    [Authorize]
    public class IndustryV1Controller : ControllerBase
    {
        private readonly IIndustryV1Service service;

        public IndustryV1Controller(IIndustryV1Service service)
        {
            this.service = service;
        }
        
        [Route("api/v1/industry/{id}")]
        [HttpGet]
        public async Task<Response<IndustryV1Dto>> GetAsync(int id)
        {
            var res = await service.FindAsync(id);
            return Response<IndustryV1Dto>.Ok(res);
        }

        [Route("api/v1/industry/page")]
        [HttpGet]
        public async Task<Response<PagedResultDto<IndustryV1Dto>>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await service.GetPageAsync(input);
            return Response<IndustryV1Dto>.Ok(res);
        }

        [Route("api/v1/industry")]
        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<Response<IndustryV1Dto>> CreateAsync([FromBody] CreateUpdateIndustryV1Dto input)
        {
            var res = await service.CreateAsync(input);
            return Response<IndustryV1Dto>.Ok(res);
        }

        [Route("api/v1/industry/{id}")]
        [HttpPut]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<Response<IndustryV1Dto>> UpdateAsync(int id, [FromBody] CreateUpdateIndustryV1Dto input)
        {
            var res = await service.UpdateAsync(id, input);
            return Response<IndustryV1Dto>.Ok(res);
        }

        [Route("api/v1/industry/{id}")]
        [HttpDelete]
        [Authorize(Roles=RoleConstants.Admin)]
        public async Task<Response> DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
            return Haxpe.Models.Response.Ok();
        }
    }
}