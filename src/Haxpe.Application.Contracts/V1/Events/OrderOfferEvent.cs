using Haxpe.V1.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Events
{
    public class OrderOfferEvent : IEventModel<OrderOffer>
    {
        public string Type => "order.offer";

        public OrderOffer Payload { get; init; }
    }

    public class OrderOffer
    {
        public Orders.OrderV1Dto Order { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
