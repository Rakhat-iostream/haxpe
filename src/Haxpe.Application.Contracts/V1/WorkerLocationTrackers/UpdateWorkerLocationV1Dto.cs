using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.WorkerLocationTrackers
{
    public class UpdateWorkerLocationV1Dto
    {
        [Required]
        public DateTimeOffset UpdateDate { get; set; }
        
        [Required]
        public double Longitude { get; set; }
        
        [Required]
        public double Latitude { get; set; }
    }
}
