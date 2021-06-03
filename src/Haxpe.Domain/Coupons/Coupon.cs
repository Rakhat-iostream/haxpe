using Haxpe.Enums;
using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Coupons
{
    public class Coupon : AggregateRoot<Guid>
    {
        public string Code { get; set; }

        public DateTimeOffset ExpirationDate { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public decimal Value { get; set; }

        public CouponUnit Unit { get; set; }

        public Coupon(Guid id, string code, DateTimeOffset expirationDate, decimal value, CouponUnit unit)
            : base(id)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            ExpirationDate = expirationDate;
            CreatedDate = DateTimeOffset.UtcNow;
            Value = value;
            Unit = unit;
        }

        private Coupon()
        {
        }
    }
}
