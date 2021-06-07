using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.WorkerLocationTrackers
{
    public class WorkerLocationTracker : AggregateRoot<Guid>
    {
        public WorkerLocationTracker(Guid id, Guid workerId, DateTimeOffset updateDate, double longitude, double latitude) : base(id)
        {
            WorkerId = workerId;
            UpdateDate = updateDate;
            Longitude = longitude;
            Latitude = latitude;
        }

        public Guid WorkerId { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
