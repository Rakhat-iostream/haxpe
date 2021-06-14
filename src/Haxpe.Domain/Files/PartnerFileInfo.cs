using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Files
{
    public class PartnerFileInfo: AggregateRoot<Guid>
    {
        public Guid PartnerId { get; set; }

        public Guid FileId { get; set; }

        private PartnerFileInfo()
        {
        }

        public PartnerFileInfo(Guid id, Guid partnerId, Guid fileId) : base(id)
        {
            PartnerId = partnerId;
            FileId = fileId;
        }
    }
}
