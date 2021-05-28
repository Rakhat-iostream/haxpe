using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public class PagedResultDto<T>
    {
        public T[] Result { get; set; }

        public int TotalCount { get; set; }
    }
}
