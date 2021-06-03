using System;
using System.ComponentModel.DataAnnotations;
using Haxpe.Enums;
using Newtonsoft.Json;

namespace Haxpe.V1.Coupons
{
    public class CreateCouponV1Dto
    {
        [Required]
        [MaxLength(256)]
        public string Code { get; set; } = null!;

        [Required]
        public DateTimeOffset ExpirationDate { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public CouponUnit Unit { get; set; }
    }
}