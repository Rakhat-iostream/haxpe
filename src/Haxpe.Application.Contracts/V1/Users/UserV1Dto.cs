using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Users
{
    public class UserV1Dto : EntityDto<Guid>
    {
        [Required]
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string FullName { get; set; }

        public Guid? PartnerId { get; set; }

        public string? PreferLanguage { get; set; }

        public bool IsExternal { get; set; }
    }
}
