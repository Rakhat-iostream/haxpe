using AutoMapper;
using Haxpe.Coupons;
using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Coupons
{
    public class CouponV1Service : ApplicationService, ICouponV1Service
    {
        private readonly IRepository<Coupon, Guid> repository;

        public CouponV1Service(IMapper mapper, IRepository<Coupon, Guid> repository) : base(mapper)
        {
            this.repository = repository;
        }

        public async Task<CouponV1Dto> CreateAsync(CreateCouponV1Dto dto)
        {
            var coupon = new Coupon(
                    Guid.NewGuid(),
                    dto.Code,
                    dto.ExpirationDate,
                    dto.Value,
                    dto.Unit
                );
            var res = await repository.CreateAsync(coupon);
            return this.mapper.Map<CouponV1Dto>(res);
        }

        public async Task DeleteAsync(Guid id)
        {
            var coupon = await this.repository.FindAsync(id);
            if(coupon == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.NotFound);
            }

            coupon.IsDeleted = true;
        }

        public async Task<CouponV1Dto> FindAsync(Guid id)
        {
            var coupon = await this.repository.FindAsync(id);
            if (coupon == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.NotFound);
            }
            return this.mapper.Map<CouponV1Dto>(coupon);
        }

        public async Task<PagedResultDto<CouponV1Dto>> GetPageAsync(CouponPagedAndSortedResultRequestDto request)
        {
            var (res, count) = (await this.repository.GetPageAsync(request.PageNumber, request.PageSize));
            return new PagedResultDto<CouponV1Dto>()
            {
                Result = res.Select(this.mapper.Map<CouponV1Dto>).ToArray(),
                TotalCount = count
            };
        }
    }
}
