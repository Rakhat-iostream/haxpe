using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public interface ICrudAppService<TEntityDto, TId, TCreateDto>: ICrudAppService<TEntityDto, TId, TCreateDto, TCreateDto>
        where TEntityDto: EntityDto<TId>
    {
    }

    public interface ICrudAppService<TEntityDto, TId, TCreateDto, TUpdateDto> : ICrudAppService<TEntityDto, TId, TUpdateDto, TCreateDto, PagedAndSortedResultRequestDto>
        where TEntityDto : EntityDto<TId>
    {
    }

    public interface ICrudAppService<TEntityDto, TId, TCreateDto, TUpdateDto, TPagedAndSortedResultRequestDto> : IApplicationService
        where TEntityDto : EntityDto<TId>
        where TPagedAndSortedResultRequestDto: PagedAndSortedResultRequestDto
    {
        Task<TEntityDto> CreateAsync(TCreateDto dto);

        Task<TEntityDto> UpdateAsync(TId id, TUpdateDto dto);

        Task<TEntityDto> FindAsync(TId id);

        Task<PagedResultDto<TEntityDto>> GetPageAsync(TPagedAndSortedResultRequestDto request);

        Task DeleteAsync(TId id);
    }
}
