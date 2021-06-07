using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.WorkerLocationTrackers
{
    public class WorkerLocationTrackerV1Dto : EntityDto<Guid>
    {
        public Guid WorkerId { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
