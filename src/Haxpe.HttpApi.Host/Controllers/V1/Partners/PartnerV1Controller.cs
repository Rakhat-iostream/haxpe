using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Haxpe.Infrastructure;
using Haxpe.Models;
using Haxpe.Partners;
using Haxpe.Roles;
using Haxpe.Users;
using Haxpe.V1.Account;
using Haxpe.V1.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Haxpe.V1.Partners
{
    [ApiVersion("1.0")]
    [ApiController]
    [ControllerName("Partner")]
    [Authorize]
    public class PartnerV1Controller : ControllerBase
    {
        private readonly UserManager<User> userManager;
        protected SignInManager<User> signInManager;
        private readonly IPartnerV1Service partnerV1Service;
        private readonly ICurrentUserService currentUserService;

        public PartnerV1Controller(
            UserManager<User> userManager,
            IPartnerV1Service partnerV1Service,
            ICurrentUserService currentUserService, 
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.partnerV1Service = partnerV1Service;
            this.currentUserService = currentUserService;
            this.signInManager = signInManager;
        }


        [Route("api/v1/partner/{id}")]
        [HttpGet]
        public async Task<Response<PartnerV1Dto>> GetAsync(Guid id)
        {
            var res = await partnerV1Service.FindAsync(id);
            return Response<PartnerV1Dto>.Ok(res);
        }

        [Route("api/v1/partner/info")]
        [HttpGet]
        public async Task<Response<PartnerV1Dto>> GetInfoAsync()
        {
            var res = await partnerV1Service.GetByUserId(await this.currentUserService.GetCurrentUserIdAsync());
            return Response<CustomerV1Dto>.Ok(res);
        }
        
        [Route("api/v1/partner/page")]
        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<Response<PagedResultDto<PartnerV1Dto>>> GetPageAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await partnerV1Service.GetPageAsync(input);
            return Response<Response<PagedResultDto<PartnerV1Dto>>>.Ok(res);
        }

        [Route("api/v1/partner")]
        [HttpGet]
        public async Task<Response<PagedResultDto<PartnerV1Dto>>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await partnerV1Service.GetPageAsync(input);
            return Response<PartnerV1Dto>.Ok(res);
        }

        [Route("api/v1/partner")]
        [HttpPost]
        public async Task<Response<PartnerV1Dto>> CreateAsync([FromBody] CreatePartnerV1Dto input)
        {
            var res = await partnerV1Service.CreateAsync(input);
            var user = await this.userManager.FindByIdAsync(res.OwnerUserId.ToString());
            (await this.userManager.AddToRoleAsync(user, RoleConstants.Partner)).CheckErrors();
            await this.signInManager.SignInAsync(user, true);
            return Response<PartnerV1Dto>.Ok(res);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [Authorize(Roles = RoleConstants.Partner)]
        [Route("api/v1/partner/{id}")]
        [HttpPut]
        public  async Task<Response<PartnerV1Dto>> UpdateAsync(Guid id, [FromBody] UpdatePartnerV1Dto input)
        {
            var res = await partnerV1Service.UpdateAsync(id, input);
            return Response<PartnerV1Dto>.Ok(res);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [Route("api/v1/partner-status/{id}/status")]
        [HttpPost]
        public async Task<Response<PartnerV1Dto>> SetStatus(Guid id, [FromBody] PartnerStatusDto input)
        {
            var res = await partnerV1Service.SetStatus(id, input);
            return Response<PartnerV1Dto>.Ok(res);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [Route("api/v1/partner/{id}")]
        [HttpDelete]
        public async Task<Response> DeleteAsync(Guid id)
        {
            await partnerV1Service.DeleteAsync(id);
            return Haxpe.Models.Response.Ok();
        }

        [Authorize(Roles = RoleConstants.Partner)]
        [Route("api/v1/partner/{id}/upload-files")]
        [HttpPost]
        public async Task<Response<IReadOnlyCollection<FileInfoDto>>> UploadFileAsync(Guid id, IFormFileCollection uploads)
        {
            var files = new List<UploadFileDto>();
            foreach (var uploadedFile in uploads)
            {
                var body = new MemoryStream();
                await uploadedFile.CopyToAsync(body);
                files.Add(new UploadFileDto
                {
                    Name = uploadedFile.FileName,
                    Type = uploadedFile.ContentType,
                    Body = body
                });
            }
            var res = await this.partnerV1Service.UploadFiles(id, files);
            return Response<IReadOnlyCollection<FileInfoDto>>.Ok(res);
        }

        [Authorize(Roles = RoleConstants.Partner + "," + RoleConstants.Admin)]
        [Route("api/v1/partner/{id}/file")]
        [HttpGet]
        public async Task<Response<IReadOnlyCollection<FileInfoDto>>> GetFilesAsync(Guid id)
        {
            var res = await this.partnerV1Service.GetFiles(id);
            return Response<IReadOnlyCollection<FileInfoDto>>.Ok(res);
        }

        [Authorize(Roles = RoleConstants.Partner + "," + RoleConstants.Admin)]
        [Route("api/v1/partner/{id}/file/{fileId}")]
        [HttpGet]
        public async Task<FileStreamResult> GetFileAsync(Guid id, Guid fileId)
        {
            var (info, stream) = await this.partnerV1Service.GetFile(id, fileId);
            return new FileStreamResult(stream, new MediaTypeHeaderValue(info.FileType))
            {
                FileDownloadName = info.FileName
            };
        }

        [Authorize(Roles = RoleConstants.Partner)]
        [Route("api/v1/partner/{id}/file/{fileId}")]
        [HttpDelete]
        public async Task<Response> DeleteFileAsync(Guid id, Guid fileId)
        {
            await this.partnerV1Service.DeleteFileAsync(id, fileId);
            return Haxpe.Models.Response.Ok();
        }
    }
}