using Haxpe.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core.Exceptions;

namespace Haxpe.Infrastructure
{
    public class GenericRepository<TRoot, TId> : IRepository<TRoot, TId>
        where TRoot : class, IAggregateRoot<TId>
        where TId : IEquatable<TId>
    {
        private const int MaxReturnSize = 500;

        private readonly DbSet<TRoot> db;

        public GenericRepository(HaxpeDbContext context)
        {
            this.db = context.Set<TRoot>();
        }

        public async Task<TRoot> CreateAsync(TRoot value)
        {
            var res = await db.AddAsync(value);
            return res.Entity;
        }

        public async Task DeleteAsync(TId id)
        {
            var root = await db.FindAsync(id);
            db.Remove(root);
        }

        public Task<TRoot> FindAsync(Expression<Func<TRoot, bool>> predicate)
        {
            return db.FirstOrDefaultAsync(predicate);
        }

        public async Task<TRoot> FindAsync(TId id)
        {
            return await db.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<TRoot>> GetListAsync(Expression<Func<TRoot, bool>> predicate = null)
        {
            var query = predicate == null ? this.db : this.db.Where(predicate);
            if (predicate != null)
            {
                var count = await query.CountAsync();
                if (count > MaxReturnSize)
                {
                    throw new BusinessException(HaxpeDomainErrorCodes.TooManyObjectsToReturn);
                }
            }

            var roots = await query.ToArrayAsync();
            return roots;
        }

        public async Task<(TRoot[], int)> GetPageAsync(int pageNumber, int pageSize, Expression<Func<TRoot, bool>> predicate = null)
        {
            if (pageSize > MaxReturnSize)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.TooManyObjectsToReturn);
            }
            var query = predicate == null ? this.db : this.db.Where(predicate);
            var roots = await query.Skip(pageNumber * pageSize).Take(pageSize).ToArrayAsync();
            var count = await query.CountAsync();

            return (roots, count);
        }

        public Task<TRoot> UpdateAsync(TRoot value)
        {
            var res = db.Update(value);
            return Task.FromResult(res.Entity);
        }
    }
}
