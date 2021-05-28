using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public abstract class CrudAppService<TRoot, TEntityDto, TId, TUpdateDto> : CrudAppService<TRoot, TEntityDto, TId, TUpdateDto, TUpdateDto, PagedAndSortedResultRequestDto>
        where TRoot : AggregateRoot<TId>
        where TEntityDto : EntityDto<TId>
    {
        protected CrudAppService(IRepository<TRoot, TId> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }

    public abstract class CrudAppService<TRoot, TEntityDto, TId, TUpdateDto, TPagedAndSortedResultRequestDto> : CrudAppService<TRoot, TEntityDto, TId, TUpdateDto, TUpdateDto, TPagedAndSortedResultRequestDto>
        where TRoot : AggregateRoot<TId>
        where TEntityDto : EntityDto<TId>
        where TPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        protected CrudAppService(IRepository<TRoot, TId> repository, IMapper mapper)
            :base(repository, mapper)
        {
        }
    }

    public abstract class CrudAppService<TRoot, TEntityDto, TId, TCreateDto, TUpdateDto, TPagedAndSortedResultRequestDto> : ApplicationService, ICrudAppService<TEntityDto, TId, TCreateDto, TUpdateDto, TPagedAndSortedResultRequestDto>
        where TRoot : AggregateRoot<TId>
        where TEntityDto : EntityDto<TId>
        where TPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        protected IRepository<TRoot, TId> Repository { get; private set; }

        protected CrudAppService(IRepository<TRoot, TId> repository, IMapper mapper)
            :base(mapper)
        {
            this.Repository = repository;
        }

        public virtual async Task<TEntityDto> CreateAsync(TCreateDto dto)
        {
            var root = this.mapper.Map<TRoot>(dto);
            var newRoot = await this.Repository.CreateAsync(root);
            return this.MapToGetOutputDto(newRoot);
        }

        public virtual async Task DeleteAsync(TId id)
        {
            await this.Repository.DeleteAsync(id);
        }

        public virtual async Task<TEntityDto> FindAsync(TId id)
        {
            var root = await this.Repository.FindAsync(id);
            return this.MapToGetOutputDto(root);
        }

        public virtual async Task<TEntityDto> UpdateAsync(TId id, TUpdateDto dto)
        {
            var root = this.mapper.Map<TRoot>(dto);
            root.Id = id;
            var newRoot = await this.Repository.UpdateAsync(root);
            return this.MapToGetOutputDto(newRoot);
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
