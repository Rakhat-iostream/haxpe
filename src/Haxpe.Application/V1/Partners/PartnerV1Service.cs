using System;
using System.Threading.Tasks;
using AutoMapper;
using Haxpe.Infrastructure;
using Haxpe.Partners;
using Haxpe.Roles;
using Haxpe.Users;

namespace Haxpe.V1.Partners
{
    public class PartnerV1Service :
        CrudAppService<Partner, PartnerV1Dto, Guid, UpdatePartnerV1Dto>,
        IPartnerV1Service
    {
        public PartnerV1Service(
            IRepository<Partner, Guid> repository,
            IMapper mapper
          ) : base(repository, mapper)
        {
        }

        public override async Task<PartnerV1Dto> UpdateAsync(Guid id, UpdatePartnerV1Dto input)
        {
            var partner = await Repository.FindAsync(id);
            CheckPermission(partner);
            return await base.UpdateAsync(id, input);
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
    }
}