using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public interface IRepository<TRoot, TId>
        where TRoot: IAggregateRoot<TId>
    {
        Task<TRoot> FindAsync(Expression<Func<TRoot, bool>> predicate);

        Task<TRoot> FindAsync(TId id);

        Task<(TRoot[], int)> GetPageAsync(int pageNumber, int pageSize, Expression<Func<TRoot, bool>> predicate = null);

        Task<IReadOnlyCollection<TRoot>> GetListAsync(Expression<Func<TRoot, bool>> predicate = null);

        Task DeleteAsync(TId id);

        Task<TRoot> UpdateAsync(TRoot value);

        Task<TRoot> CreateAsync(TRoot value);
    }
}
