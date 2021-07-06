using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;

namespace Haxpe.Workers
{
    public class WorkerServiceType: ValueObject
    {
        public Guid WorkerId { get; set; }

        public int ServiceTypeId { get; set; }
    }
}