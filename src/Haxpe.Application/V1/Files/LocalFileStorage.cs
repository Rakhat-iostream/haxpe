using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Files
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly string rootFolder = "/files";

        public Task DeleteFile(Guid id)
        {
            File.Delete($"{rootFolder}/{id}");
            return Task.CompletedTask;
        }

        public async Task<Stream> GetFile(Guid id)
        {
            var file = await File.ReadAllBytesAsync($"{rootFolder}/{id}");
            return new MemoryStream(file);
        }

        public async Task UploadFile(Guid id, Stream file)
        {
            using (var fileStream = File.Create($"{rootFolder}/{id}"))
            {
                file.Seek(0, SeekOrigin.Begin);
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
