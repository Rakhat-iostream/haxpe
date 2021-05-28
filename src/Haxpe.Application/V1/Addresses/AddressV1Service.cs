using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Haxpe.Addresses;
using Haxpe.Infrastructure;
using Haxpe.Roles;

namespace Haxpe.V1.Addresses
{
    public class AddressV1Service: CrudAppService<Address, AddressV1Dto, Guid, UpdateAddressV1Dto>,
        IAddressV1Service
    {
        public AddressV1Service(IRepository<Address, Guid> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<AddressV1Dto> CreateAsync(UpdateAddressV1Dto input)
        {
            if (!string.IsNullOrEmpty(input.ExternalId))
            {
                var address = await Repository.FindAsync(x => x.ExternalId == input.ExternalId);
                if (address != null)
                {
                    return MapToGetOutputDto(address);
                }
            }
            return await base.CreateAsync(input);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IReadOnlyCollection<AddressV1Dto>> GetListAsync(AddressListQuery query)
        {
            if (query.AddressIds?.Any() == true)
            {
                var customers = await Repository.GetListAsync(x => query.AddressIds.Contains(x.Id));
                return customers.Select(base.MapToGetOutputDto).ToArray();
            }

            return null;
        }
    }
}