using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Files
{
    public class FileInfoDto : EntityDto<Guid>
    {
        public string? FileName { get; set; }

        public string? FileType { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
