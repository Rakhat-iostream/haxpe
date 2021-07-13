using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Haxpe.Infrastructure;
using Haxpe.Partners;
using Haxpe.Roles;
using Haxpe.Users;
using Haxpe.Workers;

namespace Haxpe.V1.Workers
{
    public class WorkerV1Service :
        CrudAppService<Worker, WorkerV1Dto, Guid, UpdateWorkerV1Dto>,
        IWorkerV1Service
    {
        private readonly IRepository<Partner, Guid> _partnerRepository;

        public WorkerV1Service(
            IRepository<Worker, Guid> repository,
            IRepository<Partner, Guid> partnerRepository,
            IMapper mapper
          ) : base(repository, mapper)
        {
            _partnerRepository = partnerRepository;
        }

        public override async Task<WorkerV1Dto> CreateAsync(UpdateWorkerV1Dto input)
        {
            var partner = await _partnerRepository.FindAsync(input.PartnerId);
            CheckPartner(partner);
            return await base.CreateAsync(input);
        }

        public override async Task<WorkerV1Dto> UpdateAsync(Guid id, UpdateWorkerV1Dto input)
        {
            var partner = await _partnerRepository.FindAsync(input.PartnerId);
            CheckPartner(partner);
            return await base.UpdateAsync(id, input);
        }

        public override async Task DeleteAsync(Guid id)
        {
            var worker = await Repository.FindAsync(id);
            var partner = await _partnerRepository.FindAsync(worker.PartnerId);
            CheckPartner(partner);
            await base.DeleteAsync(id);
        }

        public async Task<IReadOnlyCollection<WorkerV1Dto>> GetListAsync(WorkerListRequestV1Dto query)
        {
                var workers = await Repository.GetListAsync(x => x.PartnerId == query.PartnerId);
                return workers.Select(base.MapToGetOutputDto).ToArray();
        }

        //protected override async Task<IQueryable<Worker>> CreateFilteredQueryAsync(WorkerListRequestV1Dto input)
        //{
        //    var partner = await _partnerRepository.FindAsync(input.PartnerId);
        //    CheckPartner(partner);
        //    var query = await base.CreateFilteredQueryAsync(input);
        //    return query.Where(x => x.PartnerId == input.PartnerId);
        //}

        private void CheckPartner(Partner partner)
        {
            //if (!(
            //        CurrentUser.IsInRole(RoleConstants.Admin) ||  
            //        (CurrentUser.IsInRole(RoleConstants.Partner)
            //        && partner.OwnerUserId == CurrentUser.Id)
            //    )
            //)
            //{
            //    throw new BusinessException("Access denied");
            //}
        }

        public async Task<WorkerV1Dto> GetByUserId(Guid id)
        {
            var worker = await this.Repository.FindAsync(x => x.UserId == id);

            if (worker == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.NotFound);
            }

            return base.MapToGetOutputDto(worker);
        }
    }
}