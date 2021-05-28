using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Haxpe.Infrastructure;
using Haxpe.V1.Partners;

namespace Haxpe.V1.Customers
{
    public interface ICustomerV1Service: ICrudAppService<CustomerV1Dto, Guid, UpdateCustomerV1Dto>
    {
        Task<CustomerV1Dto> GetByUserId(Guid id);

        Task<IReadOnlyCollection<CustomerV1Dto>> GetListAsync(CustomerListQuery query);
    }
}