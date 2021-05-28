using Haxpe.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Partners
{
    public class PartnerV1Dto: EntityDto<Guid>
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; } = null!;
        
        [Required]
        public Guid OwnerUserId { get; set; }

        public string? Description { get; set; }

        public Guid? AddressId { get; set; }
    }
}