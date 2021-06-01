using Haxpe.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;

namespace Haxpe.Users
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string FullName { get; set; }

        public Guid? PartnerId { get; set; }

        public string? PreferLanguage { get; set; }

        public bool IsExternal { get; set; }

        public void SetFullName(string name, string surname)
        {
            FullName = $"{name} {surname}";
        }

        public User()
        {
            
        }
    }
}
