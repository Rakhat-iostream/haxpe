using Haxpe.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;

namespace Haxpe.Filters
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        private static readonly string[] TransactionMethods = new string[] {
            HttpMethod.Put.Method,
            HttpMethod.Post.Method,
            HttpMethod.Delete.Method
        };
        private readonly HaxpeDbContext context;

        public UnitOfWorkFilter(HaxpeDbContext context)
        {
            this.context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (TransactionMethods.Contains(context.HttpContext.Request.Method))
            {
                using (var transaction = this.context.Database.BeginTransaction())
                {
                    try
                    {
                        var executedContext = await next.Invoke();
                        if (executedContext.Exception == null)
                        {
                            await this.context.SaveChangesAsync();
                            await transaction.CommitAsync();
                        }
                        else
                        {
                            await transaction.RollbackAsync();
                        }
                    }
                    catch(Exception ex)
                    {
                        Log.Logger.Error(ex, $"Rallback exception");
                        await transaction.RollbackAsync();
                    }
                }
            }
            else
            {
                await next.Invoke();
            }
        }
    }
}
