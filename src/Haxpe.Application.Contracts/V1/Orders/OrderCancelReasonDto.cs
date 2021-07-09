using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Orders
{
    public class OrderCancelReasonDto
    {
        [MaxLength(256)]
        public string? Reason { get; set; } = null!;
    }
}
