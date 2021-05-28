using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public class PagedAndSortedResultRequestDto
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string? SortedBy { get; set; }
    }
}
