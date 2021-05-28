using System;
using System.Collections.Generic;

namespace Haxpe.V1.Account
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsExternal { get; set; }

        public bool HasPassword { get; set; }
        
        public Guid? PartnerId { get; set; }

        public string? PreferLanguage { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}