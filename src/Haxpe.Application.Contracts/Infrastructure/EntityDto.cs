using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public class EntityDto<T>
    {
        public T Id { get; protected set; }

        protected EntityDto(T id)
        {
            Id = id;
        }

        protected EntityDto()
        {
        }
    }
}
