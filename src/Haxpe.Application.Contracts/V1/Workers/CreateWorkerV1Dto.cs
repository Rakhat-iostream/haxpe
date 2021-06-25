using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Workers
{
    public class CreateWorkerV1Dto
    {
        [Required]
        public Guid PartnerId { get; set; }

        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; } = null!;
        
        [Required]
        [MaxLength(128)]
        public string LastName { get; set; } = null!;

        [Required] 
        [MaxLength(256)]
        public string Email { get; set; } = null!;
        
        [Required] 
        [MaxLength(12)]
        public string Phone { get; set; } = null!;
        
        public ICollection<WorkerServiceTypeV1Dto> ServiceTypes { get; set; } = null!;
        
    }
}