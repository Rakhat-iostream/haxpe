using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public abstract class AggregateRoot<T>
    {
        public T Id { get; set; }

        protected AggregateRoot(T id)
        {
            Id = id;
        }

        protected AggregateRoot()
        {
        }
    }
}
