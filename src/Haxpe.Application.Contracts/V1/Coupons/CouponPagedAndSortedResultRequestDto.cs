using Haxpe.Infrastructure;

namespace Haxpe.V1.Coupons
{
    public class CouponPagedAndSortedResultRequestDto: PagedAndSortedResultRequestDto
    {
        public bool? IsActual { get; set; }
    }
}