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
    }
}