using AutoMapper;
using Haxpe.Infrastructure;
using Haxpe.WorkerLocationTrackers;
using Haxpe.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.WorkerLocationTrackers
{
    public class WorkerLocationTrackerV1Service : ApplicationService,
        IWorkerLocationTrackerV1Service
    {
        protected IRepository<WorkerLocationTracker, Guid> _repository { get; private set; }
        protected IRepository<Worker, Guid> _workerRepository { get; private set; }
        
        public WorkerLocationTrackerV1Service(IRepository<WorkerLocationTracker, Guid> repository,
            IRepository<Worker, Guid> workerRepository, IMapper mapper)
            : base(mapper)
        {
            _repository = repository;
            _workerRepository = workerRepository;
        }

        public async Task<WorkerLocationTrackerV1Dto> GetByWorkerIdAsync(Guid workerId)
        {
            var workerTracker = await _repository.FindAsync(x => x.WorkerId == workerId);

            if (workerTracker == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.NotFound);
            }
            return mapper.Map<WorkerLocationTrackerV1Dto>(workerTracker);
        }

        public async Task<WorkerLocationTrackerV1Dto> SetLocation(Guid workerId, UpdateWorkerLocationV1Dto input)
        {
            var worker = await _workerRepository.FindAsync(workerId);
            if (worker != null)
            {
                var tracker = await _repository.FindAsync(x => x.WorkerId == workerId);
                if (tracker == null)
                {
                    tracker = await _repository.CreateAsync(new WorkerLocationTracker(
                        Guid.NewGuid(), workerId, input.UpdateDate,
                        input.Longitude, input.Latitude));
                }
                else
                {
                    tracker.UpdateDate = input.UpdateDate;
                    tracker.Longitude = input.Longitude;
                    tracker.Latitude = input.Latitude;
                }
                return mapper.Map<WorkerLocationTrackerV1Dto>(tracker);
            }
            else
            {
                throw new BusinessException(HaxpeDomainErrorCodes.WorkerLocationNotFound);
            }
        }
    }
}
