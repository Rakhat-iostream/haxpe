using Haxpe.Infrastructure;
using Haxpe.Partners;
using System;
using System.Collections.Generic;
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

        public int? NumberOfWorkers { get; set; }

        public PartnerStatus partnerStatus { get; set; }

        public ICollection<PartnerIndustryV1Dto> Industries { get; set; }
    }
}