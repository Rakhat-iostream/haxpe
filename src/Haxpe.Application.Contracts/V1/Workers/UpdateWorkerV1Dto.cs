using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Workers
{
    public class UpdateWorkerV1Dto
    {
        [Required]
        public Guid PartnerId { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        
        public ICollection<WorkerServiceTypeV1Dto> WorkerServiceTypes { get; set; } = null!;
        
    }
}