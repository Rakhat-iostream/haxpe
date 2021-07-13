using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Haxpe.V1.Workers
{
    public interface IWorkerV1Service:
        ICrudAppService<WorkerV1Dto, Guid, UpdateWorkerV1Dto>
    {
        Task<IReadOnlyCollection<WorkerV1Dto>> GetListAsync(WorkerListRequestV1Dto query);

        Task<WorkerV1Dto> GetByUserId(Guid id);
    }
}