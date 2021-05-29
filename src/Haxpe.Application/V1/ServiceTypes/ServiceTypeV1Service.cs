using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Haxpe.Infrastructure;
using Haxpe.Roles;
using Haxpe.V1.Industry;
using Haxpe.V1.ServiceType;

namespace Haxpe.V1.ServiceTypes
{
    public class ServiceTypeV1Service : CrudAppService<Haxpe.ServiceTypes.ServiceType, ServiceTypeV1Dto, int, CreateUpdateServiceTypeV1Dto>,
        IServiceTypeV1Service
    {
        public ServiceTypeV1Service(IRepository<Haxpe.ServiceTypes.ServiceType, int> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<IReadOnlyCollection<ServiceTypeV1Dto>> GetAllAsync()
        {
            var serviceTypes = await Repository.GetListAsync();
            return serviceTypes.OrderBy(x => x.Id).Select(base.MapToGetOutputDto).ToArray();
        }

        public async Task<IReadOnlyCollection<ServiceTypeV1Dto>> GetListAsync(ServiceTypeListQuery query)
        {
            if (query.IndustryId.HasValue)
            {
                var serviceTypes = await Repository.GetListAsync(x => x.IndustryId == query.IndustryId);
                return serviceTypes.Select(base.MapToGetOutputDto).ToArray();
            }

            return null;
        }
    }
}