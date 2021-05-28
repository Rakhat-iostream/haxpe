using System;
using System.Linq;
using System.Threading.Tasks;
using Haxpe.Infrastructure;
using Haxpe.Models;
using Haxpe.Partners;
using Haxpe.Roles;
using Haxpe.Users;
using Haxpe.V1.Account;
using Haxpe.Workers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Haxpe.V1.Workers
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    [ControllerName("Worker")]
    public class WorkerV1Controller : ControllerBase
    {
        private readonly IWorkerV1Service workerV1Service;
        private readonly UserManager<User> _userManager;

        public WorkerV1Controller(
            IWorkerV1Service workerV1Service,
            UserManager<User> userManager
        ) 
        {
            this.workerV1Service = workerV1Service;
            _userManager = userManager;
        }

        [Route("api/v1/worker/{id}")]
        [HttpGet]
        public async Task<Response<WorkerV1Dto>> GetAsync(Guid id)
        {
            var res = await workerV1Service.FindAsync(id);
            return Response<WorkerV1Dto>.Ok(res);
        }

        [Route("api/v1/worker")]
        [HttpGet]
        public async Task<Response<PagedResultDto<WorkerV1Dto>>> GetListAsync([FromQuery] WorkerListRequestV1Dto input)
        {
            var res = await workerV1Service.GetPageAsync(input);
            return Response<WorkerV1Dto>.Ok(res);
        }

        [Route("api/v1/worker")]
        [HttpPost]
        public async Task<Response<WorkerV1Dto>> CreateAsync([FromBody] UpdateWorkerV1Dto input)
        {
            var res = await workerV1Service.CreateAsync(input);
            return Response<WorkerV1Dto>.Ok(res);
        }
        
        [Route("api/v1/worker/create-full")]
        [HttpPost]
        public async Task<Response<WorkerV1Dto>> CreateAsync([FromBody] CreateWorkerV1Dto input)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = input.Email,
                Email = input.Email,
                Name = input.FirstName,
                Surname = input.LastName,
                PhoneNumber = input.Phone,
                PartnerId = input.PartnerId
            };

            (await _userManager.CreateAsync(user, "Pass!123")).CheckErrors();

            await _userManager.AddToRoleAsync(user, RoleConstants.Partner);

            var res =  await workerV1Service.CreateAsync(new UpdateWorkerV1Dto()
            {
                PartnerId = input.PartnerId,
                UserId = Guid.Parse(user.Id),
                WorkerServiceTypes = input.WorkerServiceTypes
            });

            return Response<WorkerV1Dto>.Ok(res);
        }

        [Route("api/v1/worker/{id}")]
        [HttpPut]
        public async Task<Response<WorkerV1Dto>> UpdateAsync(Guid id, [FromBody] UpdateWorkerV1Dto input)
        {
            var res = await workerV1Service.UpdateAsync(id, input);
            return Response<WorkerV1Dto>.Ok(res);
        }

        [Route("api/v1/worker/{id}")]
        [HttpDelete]
        public async Task<Response> DeleteAsync(Guid id)
        {
            await workerV1Service.DeleteAsync(id);
            return Haxpe.Models.Response.Ok();
        }
    }
}