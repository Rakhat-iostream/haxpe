using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Haxpe.V1.Addresses
{
    public interface IAddressV1Service: ICrudAppService<AddressV1Dto, Guid, UpdateAddressV1Dto>
    {
        Task<IReadOnlyCollection<AddressV1Dto>> GetListAsync(AddressListQuery query);
    }
}