using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Statistics
{
    public class StatisticsModel
    {
        public StatisticsModel(DateTime creationDate, int count)
        {
            CreationDate = creationDate.Date;
            Count = count;
        }
        public DateTime CreationDate { get; set; }
        public int Count { get; set; }
    }
}
