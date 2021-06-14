using Haxpe.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Haxpe.V1.Statistics
{
    public class CountByDateV1Dto
    {
        [DataType(DataType.Date)]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime CreationDate{ get; set; }
        public int CreatedCount { get; set; }
    }
}
