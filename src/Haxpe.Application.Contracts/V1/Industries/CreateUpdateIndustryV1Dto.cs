using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Haxpe.V1.Industry
{
    public class CreateUpdateIndustryV1Dto
    {
        [Required]
        [MaxLength(128)]
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; } = null!;
    }
}