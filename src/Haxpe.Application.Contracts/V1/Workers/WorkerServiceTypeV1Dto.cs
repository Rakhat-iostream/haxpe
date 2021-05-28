using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Workers
{
    public class WorkerServiceTypeV1Dto
    {
        [Required]
        public int ServiceTypeId { get; set; }
    }
}