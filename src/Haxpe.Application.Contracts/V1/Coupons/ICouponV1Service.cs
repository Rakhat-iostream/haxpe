using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Haxpe.V1.Coupons
{
    public interface ICouponV1Service: IApplicationService
    {
        Task<CouponV1Dto> CreateAsync(CreateCouponV1Dto dto);

        Task<CouponV1Dto> FindAsync(Guid id);

        Task<PagedResultDto<CouponV1Dto>> GetPageAsync(CouponPagedAndSortedResultRequestDto request);

        Task DeleteAsync(Guid id);
    }
}