using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public abstract class AggregateRoot<T> : IAggregateRoot<T>
    {
        public T Id { get; set; }
        public DateTime CreationDate { get; set; }

        protected AggregateRoot(T id)
        {
            Id = id;
            CreationDate = DateTime.UtcNow;
        }

        protected AggregateRoot()
        {
        }
    }
}
