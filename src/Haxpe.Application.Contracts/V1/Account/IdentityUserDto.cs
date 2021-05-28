using Haxpe.Infrastructure;
using System;

namespace Haxpe.V1.Account
{
    public class IdentityUserDto : EntityDto<Guid>
    {
        public string UserName { get; set; }

        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Email { get; set; }
        
        public bool EmailConfirmed { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public bool PhoneNumberConfirmed { get; set; }
        
        public bool LockoutEnabled { get; set; }
        
        public DateTimeOffset? LockoutEnd { get; set; }
        
        public string ConcurrencyStamp { get; set; }
    }
}