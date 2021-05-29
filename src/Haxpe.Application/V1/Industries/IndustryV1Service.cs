using AutoMapper;
using Haxpe.Infrastructure;
using Haxpe.V1.Industry;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haxpe.V1.Industries
{
    public class IndustryV1Service :
        CrudAppService<Haxpe.Industries.Industry, IndustryV1Dto, int, CreateUpdateIndustryV1Dto>,
        IIndustryV1Service
    {
        public IndustryV1Service(IRepository<Haxpe.Industries.Industry, int> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<IReadOnlyCollection<IndustryV1Dto>> GetAllAsync()
        {
            var industries = await Repository.GetListAsync();
            return industries.OrderBy(x => x.Id).Select(base.MapToGetOutputDto).ToArray();
        }
    }
}