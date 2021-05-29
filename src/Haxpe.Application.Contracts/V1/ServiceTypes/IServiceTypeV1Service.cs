using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Haxpe.V1.ServiceType
{
    public interface IServiceTypeV1Service: ICrudAppService<ServiceTypeV1Dto, int, CreateUpdateServiceTypeV1Dto>
    {
        Task<IReadOnlyCollection<ServiceTypeV1Dto>> GetAllAsync();

        Task<IReadOnlyCollection<ServiceTypeV1Dto>> GetListAsync(ServiceTypeListQuery query);
    }
}