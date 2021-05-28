using Haxpe.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;

namespace Haxpe.Users
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public Guid? PartnerId { get; set; }

        public string? PreferLanguage { get; set; }

        public bool IsExternal { get; set; }


        public User()
        {
            
        }
    }
}
