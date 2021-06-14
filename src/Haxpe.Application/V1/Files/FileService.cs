using AutoMapper;
using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Files
{
    public class FileService : ApplicationService, IFileService
    {
        private readonly IRepository<Haxpe.Files.FileInfo, Guid> repository;
        private readonly IFileStorage fileStorage;

        public FileService(IRepository<Haxpe.Files.FileInfo, Guid> repository, IFileStorage fileStorage, IMapper mapper)
            :base(mapper)
        {
            this.repository = repository;
            this.fileStorage = fileStorage;
        }

        public Task<Stream> GetFileAsync(Guid id)
        {
            return this.fileStorage.GetFile(id);
        }

        public async Task<FileInfoDto> GetInfoAsync(Guid id)
        {
            var info = await this.repository.FindAsync(id);
            if(info == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.NotFound);
            }
            return this.mapper.Map<FileInfoDto>(info);
        }

        public async Task<IReadOnlyCollection<FileInfoDto>> GetInfoListAsync(FileInfoRequest request)
        {
            var infos = await this.repository.GetListAsync(x => request.Ids.Contains(x.Id));
            return infos.Select(this.mapper.Map<FileInfoDto>).ToArray();
        }

        public async Task DeleteFileAsync(Guid id)
        {
            var info = await this.repository.FindAsync(id);
            if (info == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.NotFound);
            }
            await this.repository.DeleteAsync(id);
            await this.fileStorage.DeleteFile(id);
        }

        public async Task<IReadOnlyCollection<FileInfoDto>> UploadAsync(IReadOnlyCollection<UploadFileDto> files)
        {
            var infos = new List<FileInfoDto>();

            foreach (var file in files)
            {
                var info = new Haxpe.Files.FileInfo(Guid.NewGuid(), file.Name, file.Type);
                var infoTask = this.repository.CreateAsync(info);

                var storage = this.fileStorage.UploadFile(info.Id, file.Body);

                await Task.WhenAll(infoTask, storage);

                infos.Add(this.mapper.Map<FileInfoDto>(info));
            }

            return infos;
        }
    }
}
