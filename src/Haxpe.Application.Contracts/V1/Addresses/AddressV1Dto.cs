using Haxpe.Infrastructure;
using System;

namespace Haxpe.V1.Addresses
{
    public class AddressV1Dto : EntityDto<Guid>
    {
        public string Country { get; set; } = null!;
        
        public string City { get; set; } = null!;
        
        public string? Street { get; set; }
        
        public string BuildingNum { get; set; } = null!;
        
        public string ZipCode { get; set; } = null!;
        
        public double Lon { get; set; }
        
        public double Lat { get; set; }
        
        public string? ExternalId { get; set; }
    }
}