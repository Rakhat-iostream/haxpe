using Haxpe.Infrastructure;
using System.Collections.Generic;

namespace Haxpe.Workers
{
    public class WorkerServiceType: ValueObject
    {
        public int WorkerId { get; set; }

        public int ServiceTypeId { get; set; }
    }
}