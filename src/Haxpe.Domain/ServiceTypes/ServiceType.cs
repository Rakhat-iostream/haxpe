using Haxpe.Infrastructure;

namespace Haxpe.ServiceTypes
{
    public class ServiceType : AggregateRoot<int>
    {
        public int IndustryId { get; private set; }
        
        public string Key { get; private set; }

        public ServiceType(
            int id,
            int industryId,
            string key)
            : base(id)
        {
            IndustryId = industryId;
            Key = key;
        }

        private ServiceType()
        {
        }
    }
}