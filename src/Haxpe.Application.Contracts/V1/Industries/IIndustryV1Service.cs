using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Haxpe.V1.Industry
{
    public interface IIndustryV1Service: ICrudAppService<IndustryV1Dto, int, CreateUpdateIndustryV1Dto>
    {
        Task<IReadOnlyCollection<IndustryV1Dto>> GetAllAsync();
    }
}