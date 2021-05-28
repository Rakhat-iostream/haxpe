using System;
using System.ComponentModel.DataAnnotations;
using Haxpe.Infrastructure;

namespace Haxpe.V1.Partners
{
    public class CustomerV1Dto: EntityDto<Guid>
    {
        [Required]
        public Guid UserId { get; set; }
        
        public Guid? AddressId { get; set; }
    }
}