using System;
using Haxpe.Infrastructure;

namespace Haxpe.Customers
{
    public class Customer: AggregateRoot<Guid>
    {
        public Customer(Guid id, Guid userId, Guid? addressId) : base(id)
        {
            UserId = userId;
            AddressId = addressId;
            CreationDate = DateTime.UtcNow;
        }
        
        private Customer() { }

        public Guid UserId { get; private set; }
        
        public Guid? AddressId { get; private set; }
        
    }
}