using Haxpe.EntityFrameworkCore;
using Haxpe.Statistics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure.Statistics
{
    public class StatisticsRepository<TRoot,T> : IStatisticsRepository<TRoot,T>
        where TRoot : AggregateRoot<T>
    {
        private readonly DbSet<TRoot> db;

        public StatisticsRepository(HaxpeDbContext context)
        {
            this.db = context.Set<TRoot>();
        }

        public async Task<IReadOnlyCollection<StatisticsModel>> GetCountByDatesAsync(DateTime startDate, DateTime endDate)
        {
            return await db.Where(x => x.CreationDate.Date >= startDate.Date && x.CreationDate.Date <= endDate)
                            .GroupBy(x => x.CreationDate.Date)
                            .OrderBy(x => x.Key.Date)
                            .Select(x =>
                             new StatisticsModel(
                                  x.Key.Date,
                                  x.Count())).ToListAsync();
        }
    }
}
