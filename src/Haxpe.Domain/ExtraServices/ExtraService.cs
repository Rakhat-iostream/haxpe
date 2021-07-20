using Haxpe.Infrastructure;
using Haxpe.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.ExtraServices
{
    public class ExtraService
    {
        public Guid OrderId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
