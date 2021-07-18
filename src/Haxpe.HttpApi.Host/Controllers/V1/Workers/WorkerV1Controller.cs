using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haxpe.Infrastructure;
using Haxpe.Models;
using Haxpe.Roles;
using Haxpe.Services;
using Haxpe.Users;
using Haxpe.V1.Account;
using Haxpe.V1.Common;
using Haxpe.V1.Constants;
using Haxpe.V1.Emails;
using Haxpe.V1.Partners;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Haxpe.V1.Workers
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    [ControllerName("Worker")]
    public class WorkerV1Controller : ControllerBase
    {
        private readonly IWorkerV1Service workerV1Service;
        private readonly IPartnerV1Service partnerV1Service;
        private readonly ICurrentUserService currentUserService;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly ICallbackUrlService _callbackUrlService;
        private readonly IPasswordsGenService _passwordsGenService;
        private readonly ILanguageSwitcherService _languageSwitcherService;

        public WorkerV1Controller(
            IWorkerV1Service workerV1Service,
            IPartnerV1Service partnerV1Service,
            UserManager<User> userManager,
             IEmailService emailService,
            ICallbackUrlService callbackUrlService,
            IPasswordsGenService passwordsGenService,
            ILanguageSwitcherService languageSwitcherService,
            ICurrentUserService currentUserService)
        {
            this.workerV1Service = workerV1Service;
            this.partnerV1Service = partnerV1Service;
            _userManager = userManager;
            _emailService = emailService;
            _callbackUrlService = callbackUrlService;
            _passwordsGenService = passwordsGenService;
            _languageSwitcherService = languageSwitcherService;
            this.currentUserService = currentUserService;
        }

        [Route("api/v1/worker/{id}")]
        [HttpGet]
        public async Task<Response<WorkerV1Dto>> GetAsync(Guid id)
        {
            var res = await workerV1Service.FindAsync(id);
            return Response<WorkerV1Dto>.Ok(res);
        }

        [Route("api/v1/worker")]
        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Partner)]
        public async Task<Response<IReadOnlyCollection<WorkerV1Dto>>> GetListAsync([FromQuery] WorkerListRequestV1Dto input)
        {
            var res = await workerV1Service.GetListAsync(input);
            return Response<IReadOnlyCollection<WorkerV1Dto>>.Ok(res);
        }

        [Route("api/v1/worker/info")]
        [HttpGet]
        public async Task<Response<WorkerV1Dto>> GetInfoAsync()
        {
            var res = await workerV1Service.GetByUserId(await this.currentUserService.GetCurrentUserIdAsync());
            return Response<WorkerV1Dto>.Ok(res);
        }

        [Route("api/v1/worker/page")]
        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<Response<PagedResultDto<WorkerV1Dto>>> GetPageAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await workerV1Service.GetPageAsync(input);
            return Response<Response<PagedResultDto<WorkerV1Dto>>>.Ok(res);
        }

        [Route("api/v1/worker")]
        [HttpPost]
        public async Task<Response<WorkerV1Dto>> CreateAsync([FromBody] UpdateWorkerV1Dto input)
        {
            var res = await workerV1Service.CreateAsync(input);
            return Response<WorkerV1Dto>.Ok(res);
        }
        
        [Route("api/v1/worker/create-full")]
        [HttpPost]
        public async Task<Response<WorkerV1Dto>> CreateAsync([FromBody] CreateWorkerV1Dto input)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = input.Email,
                Email = input.Email,
                Name = input.FirstName,
                Surname = input.LastName,
                PhoneNumber = input.Phone,
                PartnerId = input.PartnerId
            };
            user.SetFullName(user.Name, user.Surname);

            var password = await _passwordsGenService.GetRandomAlphaNumeric();
            (await _userManager.CreateAsync(user, password)).CheckErrors();

            await _userManager.AddToRoleAsync(user, RoleConstants.Worker);

            var res =  await workerV1Service.CreateAsync(new UpdateWorkerV1Dto()
            {
                PartnerId = input.PartnerId,
                UserId = user.Id,
                ServiceTypes = input.ServiceTypes
            });

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = _callbackUrlService.GetUrl(new CallbackUrlModel()
            {
                Path = FrontUrls.WorkerConfirmEmailCallback
            }, new { Id = user.Id, code = code });

            var partnerName = await partnerV1Service.FindAsync(input.PartnerId);

            var language = await _languageSwitcherService.SetLanguage(user.PreferLanguage);
            await _emailService.SendWorkerRegistrationConfirm(user.Email, language, new WorkerRegistrationConfirmModel()
            {
                PartnerName = $"{partnerName.Name};",
                WorkerName = $"{user.FullName}",
                Password = password,
                ConfirmLink = callbackUrl
            });


            return Response<WorkerV1Dto>.Ok(res);
        }

        [Route("api/v1/worker/{id}")]
        [HttpPut]
        public async Task<Response<WorkerV1Dto>> UpdateAsync(Guid id, [FromBody] UpdateWorkerV1Dto input)
        {
            var res = await workerV1Service.UpdateAsync(id, input);
            return Response<WorkerV1Dto>.Ok(res);
        }

        [Route("api/v1/worker/{id}")]
        [HttpDelete]
        public async Task<Response> DeleteAsync(Guid id)
        {
            await workerV1Service.DeleteAsync(id);
            return Haxpe.Models.Response.Ok();
        }
    }
}