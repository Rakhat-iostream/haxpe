using System;
using Haxpe.Infrastructure;
using Haxpe.Orders;

namespace Haxpe.V1.Orders
{
    public class OrderV1Dto: EntityDto<Guid>
    {
        public Guid? PartnerId { get; set; }
        
        public Guid? WorkerId { get; set; }
        
        public Guid CustomerId { get; set; }
        
        public Guid AddressId { get; set; }
        
        public DateTimeOffset OrderDate { get; set; }
        
        public int IndustryId { get; set; }
        
        public int ServiceTypeId { get; set; }
        
        public int? DurationSecond { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public decimal? NetAmount { get; set; }

        public decimal Tax { get; set; }
        
        public decimal? TotalAmount { get; set; }
        
        public float? Rating { get; set; }
        
        public string? Comment { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public Guid? CouponId { get; set; }

        public string? CouponCode { get; set; }
    }
}