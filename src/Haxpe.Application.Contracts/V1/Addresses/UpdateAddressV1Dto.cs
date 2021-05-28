using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Addresses
{
    public class UpdateAddressV1Dto
    {
        [Required]
        public string Country { get; set; } = null!;
        
        [Required]
        public string City { get; set; } = null!;
        
        public string? Street { get; set; }
        
        [Required]
        public string BuildingNum { get; set; } = null!;
        
        [Required]
        public string ZipCode { get; set; } = null!;
        
        [Required]
        public double Lon { get; set; }
        
        [Required]
        public double Lat { get; set; }
        
        public string? ExternalId { get; set; }
    }
}