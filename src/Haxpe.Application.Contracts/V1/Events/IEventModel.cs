using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Events
{
    public interface IEventModel<TPayload>
    {
        string Type { get; }

        TPayload Payload { get; }
    }
}
