using Haxpe.Infrastructure;
using System;

namespace Haxpe.V1.Workers
{
    public interface IWorkerV1Service:
        ICrudAppService<WorkerV1Dto, Guid, UpdateWorkerV1Dto>
    {
        
    }
}