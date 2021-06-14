using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Files
{
    public interface IFileService
    {
        Task<FileInfoDto> GetInfoAsync(Guid id);

        Task<IReadOnlyCollection<FileInfoDto>> GetInfoListAsync(FileInfoRequest request);

        Task<Stream> GetFileAsync(Guid id);

        Task DeleteFileAsync(Guid id);

        Task<IReadOnlyCollection<FileInfoDto>> UploadAsync(IReadOnlyCollection<UploadFileDto> files);
    }
}
