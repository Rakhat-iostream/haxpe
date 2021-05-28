using AutoMapper;
using Haxpe.Infrastructure;
using Haxpe.V1.Industry;

namespace Haxpe.V1.Industries
{
    public class IndustryV1Service :
        CrudAppService<Haxpe.Industries.Industry, IndustryV1Dto, int, CreateUpdateIndustryV1Dto>,
        IIndustryV1Service
    {
        public IndustryV1Service(IRepository<Haxpe.Industries.Industry, int> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}