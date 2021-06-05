using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public interface IReadAppService<TEntityDto, TId> : IReadAppService<TEntityDto, TId, PagedAndSortedResultRequestDto>
           where TEntityDto : EntityDto<TId>
    {
    }

    public interface IReadAppService<TEntityDto, TId, TPagedAndSortedResultRequestDto> : IApplicationService
        where TEntityDto : EntityDto<TId>
        where TPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        Task<TEntityDto> FindAsync(TId id);
        Task<TEntityDto> GetAsync(TId id);
        Task<PagedResultDto<TEntityDto>> GetPageAsync(TPagedAndSortedResultRequestDto request);
    }
}
