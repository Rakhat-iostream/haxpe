using Haxpe.Partners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Partners
{
    public class UpdatePartnerV1Dto
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; } = null!;

        [MaxLength(512)]
        public string? Description { get; set; }

        public Guid? AddressId { get; set; }
        
        [Required]
        public Guid OwnerId { get; set; }

        public int? NumberOfWorkers { get; set; }

        public PartnerStatus PartnerStatus { get; set; }

        public ICollection<PartnerIndustryV1Dto> Industries { get; set; }
    }
}