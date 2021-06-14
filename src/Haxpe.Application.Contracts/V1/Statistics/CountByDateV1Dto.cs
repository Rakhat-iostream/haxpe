using Haxpe.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Statistics
{
    public class CountByDateV1Dto
    {
        [DataType(DataType.Date)]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime CreationDate{ get; set; }

        public int Count { get; set; }
    }
}
