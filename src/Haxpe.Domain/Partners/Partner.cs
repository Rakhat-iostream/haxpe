using Haxpe.Infrastructure;
using System;

namespace Haxpe.Partners
{
    public class Partner : AggregateRoot<Guid>
    {
        public string Name { get; private set; }
        
        public Guid OwnerUserId { get; private set; }

        public string? Description { get; set; }

        public Guid? AddressId { get; set; }
        

        public Partner(
            Guid id,
            string name, 
            Guid ownerUserId,
            string? description = null, 
            Guid? addressId = null)
            :base(id)
        {
            Name = name;
            OwnerUserId = ownerUserId;
            Description = description;
            AddressId = addressId;
        }

        private Partner()
        {
        }
    }
}