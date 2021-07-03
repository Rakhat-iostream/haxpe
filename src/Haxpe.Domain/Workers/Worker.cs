using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Haxpe.Workers
{
    public class Worker: AggregateRoot<Guid>
    {
        public Worker(Guid partnerId, Guid userId)
        {
            PartnerId = partnerId;
            UserId = userId;
            ServiceTypes = new Collection<WorkerServiceType>();
            CreationDate = DateTime.UtcNow;
        }
        
        private Worker() { }

        public Guid PartnerId { get; private set;}
        
        public Guid UserId { get; private set; }

        public ICollection<WorkerServiceType>? ServiceTypes { get; set; }
    }
}