using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Haxpe.Files;
using Haxpe.Infrastructure;
using Haxpe.Partners;
using Haxpe.Roles;
using Haxpe.Users;
using Haxpe.V1.Files;

namespace Haxpe.V1.Partners
{
    public class PartnerV1Service :
        CrudAppService<Partner, PartnerV1Dto, Guid, CreatePartnerV1Dto, UpdatePartnerV1Dto, PagedAndSortedResultRequestDto>,
        IPartnerV1Service
    {
        private readonly IRepository<PartnerFileInfo, Guid> partnerFileReporistory;
        private readonly IFileService fileService;

        public PartnerV1Service(
            IRepository<Partner, Guid> repository,
            IRepository<PartnerFileInfo, Guid> partnerFileReporistory,
            IFileService fileService,
            IMapper mapper
          ) : base(repository, mapper)
        {
            this.partnerFileReporistory = partnerFileReporistory;
            this.fileService = fileService;
        }

        public override async Task<PartnerV1Dto> UpdateAsync(Guid id, UpdatePartnerV1Dto input)
        {
            var partner = await Repository.FindAsync(id);
            CheckPermission(partner);
            return await base.UpdateAsync(id, input);
        }

        public async Task<IReadOnlyCollection<FileInfoDto>> GetFiles(Guid partnerId)
        {
            var partnerFiles = await this.partnerFileReporistory.GetListAsync(x => x.PartnerId == partnerId);
            var files = await this.fileService.GetInfoListAsync(new FileInfoRequest { Ids = partnerFiles.Select(x => x.FileId).ToList() });
            return files;
        }

        public async Task<IReadOnlyCollection<FileInfoDto>> UploadFiles(Guid partnerId, IReadOnlyCollection<UploadFileDto> files)
        {
            var res = await this.fileService.UploadAsync(files);
            var tasks = res.Select(x => this.partnerFileReporistory.CreateAsync(new PartnerFileInfo(
                Guid.NewGuid(),
                partnerId,
                x.Id
                ))).ToArray();
            await Task.WhenAll(tasks);

            return res;
        }

        public async Task<PartnerV1Dto> GetByUserId(Guid id)
        {
            var partner = await this.Repository.FindAsync(x => x.OwnerUserId == id);

            if (partner == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.NotFound);
            }

            return base.MapToGetOutputDto(partner);
        }

        private void CheckPermission(Partner partner = null)
        {

            //if (!(
            //           CurrentUser.IsInRole(RoleConstants.Admin) ||
            //           (CurrentUser.IsInRole(RoleConstants.Partner)
            //            && partner.OwnerUserId != CurrentUser.Id)
            //       )
            //   )
            //{
            //    throw new BusinessException("Access denied");
            //}

        }

        public async Task<(FileInfoDto, Stream)> GetFile(Guid partnerId, Guid fileId)
        {
            var info = await this.fileService.GetInfoAsync(fileId);
            var stream = await this.fileService.GetFileAsync(fileId);
            return (info, stream);
        }

        public async Task DeleteFileAsync(Guid partnerId, Guid fileId)
        {
            var partnerFile = await this.partnerFileReporistory.FindAsync(x => x.PartnerId == partnerId && x.FileId == fileId);
            if(partnerFile == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.NotFound);
            }
            await this.partnerFileReporistory.DeleteAsync(partnerFile.Id);
            await this.fileService.DeleteFileAsync(fileId);
        }
    }
}