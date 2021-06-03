using System;
using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Orders
{
    public class ApplyCouponDto
    {   
        [Required]
        public string Code { get; set; } = null!;
    }
}