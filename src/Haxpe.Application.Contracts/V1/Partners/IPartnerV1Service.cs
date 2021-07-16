using Haxpe.Infrastructure;
using Haxpe.Partners;
using Haxpe.V1.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Haxpe.V1.Partners
{
    public interface IPartnerV1Service: ICrudAppService<PartnerV1Dto, Guid, CreatePartnerV1Dto, UpdatePartnerV1Dto>
    {
        Task<PartnerV1Dto> GetByUserId(Guid id);

        Task<IReadOnlyCollection<FileInfoDto>> GetFiles(Guid partnerId);

        Task<PartnerV1Dto> SetStatus(Guid id, PartnerStatusDto input);

        Task<(FileInfoDto, Stream)> GetFile(Guid partnerId, Guid fileId);

        Task DeleteFileAsync(Guid partnerId, Guid fileId);

        Task<IReadOnlyCollection<FileInfoDto>> UploadFiles(Guid partnerId, IReadOnlyCollection<UploadFileDto> files);


    }
}