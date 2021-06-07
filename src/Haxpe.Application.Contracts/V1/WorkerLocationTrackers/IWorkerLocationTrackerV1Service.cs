using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.WorkerLocationTrackers
{
    public interface IWorkerLocationTrackerV1Service : IApplicationService
    {
        Task<WorkerLocationTrackerV1Dto> GetByWorkerIdAsync(Guid workerId);
        Task<WorkerLocationTrackerV1Dto> SetLocation(Guid workerId, UpdateWorkerLocationV1Dto input);
    }
}
