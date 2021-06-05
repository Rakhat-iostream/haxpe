using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public abstract class ReadAppService<TRoot, TEntityDto, TId> : ReadAppService<TRoot, TEntityDto, TId, PagedAndSortedResultRequestDto>
           where TRoot : IAggregateRoot<TId>
           where TEntityDto : EntityDto<TId>
    {
        protected ReadAppService(IRepository<TRoot, TId> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }


    public abstract class ReadAppService<TRoot, TEntityDto, TId, TPagedAndSortedResultRequestDto> : ApplicationService, IReadAppService<TEntityDto, TId, TPagedAndSortedResultRequestDto>
        where TRoot : IAggregateRoot<TId>
        where TEntityDto : EntityDto<TId>
        where TPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        protected IRepository<TRoot, TId> Repository { get; private set; }

        protected ReadAppService(IRepository<TRoot, TId> repository, IMapper mapper)
            : base(mapper)
        {
            this.Repository = repository;
        }

        public virtual async Task<TEntityDto> FindAsync(TId id)
        {
            var root = await this.Repository.FindAsync(id);
            return this.MapToGetOutputDto(root);
        }

        public virtual async Task<TEntityDto> GetAsync(TId id)
        {
            var root = await Repository.FindAsync(id);
            if (root == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.NotFound);
            }

            return this.MapToGetOutputDto(root);
        }

        protected TEntityDto MapToGetOutputDto(TRoot root)
        {
            return this.mapper.Map<TEntityDto>(root);
        }

        public virtual async Task<PagedResultDto<TEntityDto>> GetPageAsync(TPagedAndSortedResultRequestDto request)
        {
            var (res, count) = await this.Repository.GetPageAsync(request.PageNumber, request.PageSize);
            return new PagedResultDto<TEntityDto>()
            {
                Result = res.Select(this.MapToGetOutputDto).ToArray(),
                TotalCount = count
            };
        }
    }
}
