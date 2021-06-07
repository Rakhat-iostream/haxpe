using Haxpe.Models;
using Haxpe.Roles;
using Haxpe.V1.WorkerLocationTrackers;
using Haxpe.V1.Workers;
using Haxpe.Workers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haxpe.Controllers.V1.WorkerLocationTrackers
{
    [ApiController]
    [ControllerName("WorkerLocationTracker")]
    public class WorkerLocationTrackerV1Controller : ControllerBase
    {
        private readonly IWorkerLocationTrackerV1Service _trackerService;
        public WorkerLocationTrackerV1Controller(IWorkerLocationTrackerV1Service trackerService)
        {
            _trackerService = trackerService;
        }

        [Route("api/v1/worker/{workerId}/location")]
        [HttpGet]
        public async Task<Response<WorkerLocationTrackerV1Dto>> GetAsync(Guid workerId)
        {
            var res = await _trackerService.GetByWorkerIdAsync(workerId);
            return Response<WorkerLocationTrackerV1Dto>.Ok(res);
        }

        [Route("api/v1/worker/{workerId}/location/set")]
        [HttpPost]
        [Authorize(Roles = RoleConstants.Worker)]
        public async Task<Response<WorkerLocationTrackerV1Dto>> SetLocation(Guid workerId, [FromBody] UpdateWorkerLocationV1Dto input)
        {
            var res = await _trackerService.SetLocation(workerId,input);
            return Response<WorkerLocationTrackerV1Dto>.Ok(res);
        }
    }
}
