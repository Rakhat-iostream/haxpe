using Haxpe.Infrastructure;
using Haxpe.Models;
using Haxpe.Roles;
using Haxpe.Users;
using Haxpe.V1.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haxpe.Controllers.V1.Users
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }   

        [Route("api/v1/user/{id}")]
        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Worker + "," + RoleConstants.Partner)]
        public async Task<Response<UserV1Dto>> GetAsync(Guid id)
        {
            var res = await _service.GetAsync(id);
            return Response<UserV1Dto>.Ok(res);
        }

        [Route("api/v1/user/page")]
        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Worker + "," + RoleConstants.Partner)]
        public async Task<Response<PagedResultDto<UserV1Dto>>> GetPageAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await _service.GetPageAsync(input);
            return Response<Response<PagedResultDto<UserV1Dto>>>.Ok(res);
        }

        [Route("api/v1/user")]
        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Worker + "," + RoleConstants.Partner)]
        public async Task<Response<IReadOnlyCollection<UserV1Dto>>> GetListAsync([FromQuery] UserListQuery input)
        {
            var res = await _service.GetListAsync(input);
            return Response<Response<IReadOnlyCollection<UserV1Dto>>>.Ok(res);
        }
    }
}
