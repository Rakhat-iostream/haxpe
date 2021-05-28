using System;
using System.ComponentModel.DataAnnotations;
using Haxpe.Infrastructure;
using Newtonsoft.Json;

namespace Haxpe.V1.Industry
{
    public class IndustryV1Dto: EntityDto<int>
    {
        [Required]
        [MaxLength(128)]
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; } = null!;
    }
}