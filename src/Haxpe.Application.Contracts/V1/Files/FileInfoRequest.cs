using System;
using System.Collections.Generic;

namespace Haxpe.V1.Files
{
    public class FileInfoRequest
    {
        public IReadOnlyCollection<Guid> Ids { get; set; }
    }
}