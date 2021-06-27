using Haxpe.V1.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Events
{
    public class OrderChangedEvent : IEventModel<Orders.OrderV1Dto>
    {
        public string Type => "order.changed";

        public OrderV1Dto Payload { get; init; }
    }
}
