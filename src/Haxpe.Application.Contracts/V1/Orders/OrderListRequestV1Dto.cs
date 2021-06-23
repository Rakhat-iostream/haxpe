using System;
using Haxpe.Orders;

namespace Haxpe.V1.Orders
{
    public class OrderListRequestV1Dto
    {
        public Guid? WorkerId { get; set; }
        
        public Guid? CustomerId { get; set; }
        
        public OrderStatus? Status { get; set; }

        public bool? IsActive { get; set; }
    }
}