using System;
using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Partners
{
    public class CreatePartnerV1Dto
    {

        [Required]
        [MaxLength(128)]
        public string Name { get; set; } = null!;

        [MaxLength(512)]
        public string? Description { get; set; }

        public Guid? AddressId { get; set; }

        [Required]
        public Guid OwnerUserId { get; set; }
    }
}