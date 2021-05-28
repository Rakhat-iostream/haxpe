using Haxpe.Infrastructure;
using System;

namespace Haxpe.Industries
{
    public class Industry: AggregateRoot<int>
    {
        public string Key { get; private set; }
        
        public Industry(
            int id,
            string key)
            :base(id)
        {
            Key = key;
        }

        private Industry()
        {
        }
    }
}