using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;

namespace Haxpe.V1.Workers
{
    public class WorkerV1Dto: EntityDto<Guid>
    {
        public Guid PartnerId { get; set; }
        
        public Guid UserId { get; set; }
        
        public ICollection<WorkerServiceTypeV1Dto> ServiceTypes { get; set; } = null!;
    }
}