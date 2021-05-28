using System;
using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Partners
{
    public class UpdateCustomerV1Dto
    {
        [Required]
        public Guid UserId { get; set; }
        
        public Guid? AddressId { get; set; }
    }
}