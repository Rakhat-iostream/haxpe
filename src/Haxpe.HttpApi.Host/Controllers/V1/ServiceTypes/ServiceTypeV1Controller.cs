using System.Linq;
using System.Threading.Tasks;
using Haxpe.Infrastructure;
using Haxpe.Models;
using Haxpe.Roles;
using Haxpe.V1.Industry;
using Haxpe.V1.ServiceType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Haxpe.V1.ServiceTypes
{
    [ApiVersion("1.0")]
    [ApiController]
    [ControllerName("ServiceType")]
    [Authorize]
    public class ServiceTypeV1Controller : ControllerBase
    {
        private readonly IServiceTypeV1Service service;

        public ServiceTypeV1Controller(IServiceTypeV1Service service)
        {
            this.service = service;
        }

        [Route("api/v1/service-type/{id}")]
        [HttpGet]
        public async Task<Response<ServiceTypeV1Dto>> GetAsync(int id)
        {
            var res = await service.FindAsync(id);
            return Response<ServiceTypeV1Dto>.Ok(res);
        }

        [Route("api/v1/service-type")]
        [HttpGet]
        public async Task<Response<PagedResultDto<ServiceTypeV1Dto>>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await service.GetPageAsync(input);
            return Response<ServiceTypeV1Dto>.Ok(res);
        }

        [Route("api/v1/service-type")]
        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<Response<ServiceTypeV1Dto>> CreateAsync([FromBody] CreateUpdateServiceTypeV1Dto input)
        {
            var res = await service.CreateAsync(input);
            return Response<ServiceTypeV1Dto>.Ok(res);
        }

        [Route("api/v1/service-type/{id}")]
        [HttpPut]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<Response<ServiceTypeV1Dto>> UpdateAsync(int id, [FromBody] CreateUpdateServiceTypeV1Dto input)
        {
            var res = await service.UpdateAsync(id, input);
            return Response<ServiceTypeV1Dto>.Ok(res);
        }

        [Route("api/v1/service-type/{id}")]
        [HttpDelete]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<Response> DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
            return Haxpe.Models.Response.Ok();
        }
    }
}