using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Files
{
    public class UploadFileDto
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public Stream Body { get; set; }
    }
}
