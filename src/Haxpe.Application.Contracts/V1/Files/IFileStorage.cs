using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Files
{
    public interface IFileStorage
    {
        Task<Stream> GetFile(Guid id);

        Task UploadFile(Guid id, Stream file);

        Task DeleteFile(Guid id);
    }
}
