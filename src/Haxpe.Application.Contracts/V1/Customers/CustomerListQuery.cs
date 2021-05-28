using System;

namespace Haxpe.V1.Customers
{
    public class CustomerListQuery
    {
        public Guid[] CustomerIds { get; set; }

        public Guid[] UserIds { get; set; }
    }
}