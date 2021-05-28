using System;

namespace Haxpe.V1.Emails
{
    public class ThanksForOrderModel
    {
        public string CustomerName { get; set; }

        public Guid OrderId { get; set; }
    }
}