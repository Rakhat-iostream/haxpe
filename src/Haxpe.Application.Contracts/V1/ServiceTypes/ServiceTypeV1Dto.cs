using System;
using System.ComponentModel.DataAnnotations;
using Haxpe.Infrastructure;
using Newtonsoft.Json;

namespace Haxpe.V1.ServiceType
{
    public class ServiceTypeV1Dto: EntityDto<int>
    {
        [Required]
        public int IndustryId { get; set; }
        
        [Required]
        [MaxLength(128)]
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; } = null!;
    }
}