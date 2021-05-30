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
        [MaxLength(128)]
        public string OwnerEmail { get; set; } = null!;
        
        [Required]
        [MaxLength(12)]
        public string OwnerPhone { get; set; } = null!;
        
        [Required]
        [MaxLength(128)]
        public string OwnerFirstName { get; set; } = null!;
        
        [Required]
        [MaxLength(128)]
        public string OwnerLastName { get; set; } = null!;
    }
}