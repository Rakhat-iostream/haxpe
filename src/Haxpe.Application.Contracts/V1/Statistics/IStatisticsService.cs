using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Statistics
{
    public interface IStatisticsService : IApplicationService
    {
        Task<IReadOnlyCollection<CountByDateV1Dto>> GetOrderCountByDatesAsync(CountByDatesQuery query);
        Task<IReadOnlyCollection<CountByDateV1Dto>> GetPartnerCountByDatesAsync(CountByDatesQuery query);
        Task<IReadOnlyCollection<CountByDateV1Dto>> GetCustomerCountByDatesAsync(CountByDatesQuery query);
        Task<IReadOnlyCollection<CountByDateV1Dto>> GetWorkerCountByDatesAsync(CountByDatesQuery query);
    }
}
