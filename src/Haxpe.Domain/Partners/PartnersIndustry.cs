using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Partners
{
    public class PartnersIndustry : ValueObject
    {
        public Guid PartnerId { get; set; }
        public int IndustryId { get; set; }
    }
}
