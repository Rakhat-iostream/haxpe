using Haxpe.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Workers
{
    public class WorkerListRequestV1Dto
    {
        [Required]
        public Guid PartnerId { get; set; }
    }
}