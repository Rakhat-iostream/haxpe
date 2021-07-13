using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Workers
{
    public interface IWorkerNotifierService
    {
        Task OrderOfferNotify(Guid orderId);
    }
}
