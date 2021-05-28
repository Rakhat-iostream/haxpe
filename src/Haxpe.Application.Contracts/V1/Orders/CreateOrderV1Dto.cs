using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Haxpe.Orders;

namespace Haxpe.V1.Orders
{
    public class CreateOrderV1Dto
    {
        [Required]
        public Guid CustomerId { get; set; }
        
        [Required]
        public Guid AddressId { get; set; }
        
        [Required]
        public int ServiceTypeId { get; set; }
        
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
    }
}