using Haxpe.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure.Statistics
{
    public interface IStatisticsRepository<TRoot, T>
        where TRoot : AggregateRoot<T>
    {
        Task<IReadOnlyCollection<StatisticsModel>> GetCountByDatesAsync(DateTime startDate, DateTime endDate);
    }
}
