using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Files
{
    public class FileInfo : AggregateRoot<Guid>
    {
        public string? FileName { get; set; }

        public string? FileType { get; set; }

        private FileInfo()
        {
        }

        public FileInfo(Guid id, string? fileName, string? fileType) : base(id)
        {
            FileName = fileName;
            FileType = fileType;
            CreationDate = DateTime.UtcNow;
        }
    }
}
