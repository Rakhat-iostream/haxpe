using System;
using System.ComponentModel.DataAnnotations;
using Haxpe.Enums;
using Haxpe.Infrastructure;
using Newtonsoft.Json;

namespace Haxpe.V1.Coupons
{
    public class CouponV1Dto: EntityDto<Guid>
    {
        public string Code { get; set; }

        public DateTimeOffset ExpirationDate { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public decimal Value { get; set; }

        public CouponUnit Unit { get; set; }
    }
}