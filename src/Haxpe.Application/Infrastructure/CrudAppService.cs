using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public abstract class CrudAppService<TRoot, TEntityDto, TId, TUpdateDto> : CrudAppService<TRoot, TEntityDto, TId, TUpdateDto, TUpdateDto, PagedAndSortedResultRequestDto>
        where TRoot : IAggregateRoot<TId>
        where TEntityDto : EntityDto<TId>
    {
        protected CrudAppService(IRepository<TRoot, TId> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }

    public abstract class CrudAppService<TRoot, TEntityDto, TId, TUpdateDto, TPagedAndSortedResultRequestDto> : CrudAppService<TRoot, TEntityDto, TId, TUpdateDto, TUpdateDto, TPagedAndSortedResultRequestDto>
        where TRoot :  IAggregateRoot<TId>
        where TEntityDto : EntityDto<TId>
        where TPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        protected CrudAppService(IRepository<TRoot, TId> repository, IMapper mapper)
            :base(repository, mapper)
        {
        }
    }

    public abstract class CrudAppService<TRoot, TEntityDto, TId, TCreateDto, TUpdateDto, TPagedAndSortedResultRequestDto> : ReadAppService<TRoot, TEntityDto, TId, TPagedAndSortedResultRequestDto>, ICrudAppService<TEntityDto, TId, TCreateDto, TUpdateDto, TPagedAndSortedResultRequestDto>
        where TRoot : IAggregateRoot<TId>
        where TEntityDto : EntityDto<TId>
        where TPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        protected CrudAppService(IRepository<TRoot, TId> repository, IMapper mapper)
               : base(repository, mapper)
        {
        }

        public virtual async Task<TEntityDto> CreateAsync(TCreateDto dto)
        {
            var root = this.mapper.Map<TRoot>(dto);
            root.CreationDate = DateTime.UtcNow;
            var newRoot = await this.Repository.CreateAsync(root);
            return this.MapToGetOutputDto(newRoot);
        }

        public virtual async Task DeleteAsync(TId id)
        {
            await this.Repository.DeleteAsync(id);
        }

        public virtual async Task<TEntityDto> UpdateAsync(TId id, TUpdateDto dto)
        {
            var root = this.mapper.Map<TRoot>(dto);
            root.Id = id;
            var newRoot = await this.Repository.UpdateAsync(root);
            return this.MapToGetOutputDto(newRoot);
        }
    }
}
