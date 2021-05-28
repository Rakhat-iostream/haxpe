using Haxpe.Infrastructure;
using System;

namespace Haxpe.Addresses
{
    public class Address: AggregateRoot<Guid>
    {
        public string Country { get; private set; }
        public string City { get; private set; }
        public string? Street { get; private set; }
        public string BuildingNum { get; private set; }
        public string ZipCode { get; private set; }
        public double Lon { get; private set; }
        public double Lat { get; private set; }
        public string? ExternalId { get; private set; }

        public Address(
            Guid id,
            string country,
            string city,
            string? street,
            string buildingNum,
            string zipCode,
            double lon,
            double lat,
            string? externalId
        ) : base(id)
        {
            Country = country;
            City = city;
            Street = street;
            BuildingNum = buildingNum;
            ZipCode = zipCode;
            Lon = lon;
            Lat = lat;
            ExternalId = externalId;
        }
        
        private Address() { }
    }
}