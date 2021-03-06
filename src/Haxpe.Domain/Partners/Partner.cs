using Haxpe.Enums;
using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Haxpe.Partners
{
    public class Partner : AggregateRoot<Guid>
    {
        public string Name { get; private set; }
        
        public Guid OwnerUserId { get; private set; }

        public string? Description { get; set; }

        public Guid? AddressId { get; set; }

        public int? NumberOfWorkers { get; set; }

        public PartnerStatus? PartnerStatus { get; set; }

        public ICollection<PartnersIndustry> Industries { get; set; }


        public Partner(
            Guid id,
            string name, 
            Guid ownerUserId,
            string? description = null, 
            Guid? addressId = null,
            int? numberOfWorkers = null,
            PartnerStatus? partnerStatus = null)
            :base(id)
        {
            Name = name;
            OwnerUserId = ownerUserId;
            Description = description;
            AddressId = addressId;
            NumberOfWorkers = numberOfWorkers;
            CreationDate = DateTime.UtcNow;
            PartnerStatus = partnerStatus;
            Industries = new Collection<PartnersIndustry>();
        }

        private Partner()
        {
        }

        public void ChangeStatus(PartnerStatus? status)
        {
            PartnerStatus = status;
        }
    }
}