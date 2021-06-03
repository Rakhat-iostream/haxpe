using Haxpe.Infrastructure;
using Haxpe.Models;
using Haxpe.Roles;
using Haxpe.V1.Coupons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haxpe.Controllers.V1.Coupons
{
    [ApiVersion("1.0")]
    [ApiController]
    [ControllerName("Coupon")]
    [Authorize]
    public class CouponV1Controller : ControllerBase
    {
        private readonly ICouponV1Service service;

        public CouponV1Controller(ICouponV1Service service)
        {
            this.service = service;
        }

        [Route("api/v1/coupon/{id}")]
        [HttpGet]
        public async Task<Response<CouponV1Dto>> GetAsync(Guid id)
        {
            var res = await service.FindAsync(id);
            return Response<CouponV1Dto>.Ok(res);
        }

        [Route("api/v1/coupon/page")]
        [HttpGet]
        public async Task<Response<PagedResultDto<CouponV1Dto>>> GetPageAsync([FromQuery] CouponPagedAndSortedResultRequestDto input)
        {
            var res = await service.GetPageAsync(input);
            return Response<CouponV1Dto>.Ok(res);
        }

        [Route("api/v1/coupon")]
        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<Response<CouponV1Dto>> CreateAsync([FromBody] CreateCouponV1Dto input)
        {
            var res = await service.CreateAsync(input);
            return Response<CouponV1Dto>.Ok(res);
        }

        [Route("api/v1/coupon/{id}")]
        [HttpDelete]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<Response> DeleteAsync(Guid id)
        {
            await service.DeleteAsync(id);
            return Haxpe.Models.Response.Ok();
        }
    }
}
