using Haxpe.EntityFrameworkCore;
using Haxpe.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.DbMigrator.Seeds
{
    public class RoleSeedData
    {
        private HaxpeMigrationsDbContext _context;

        public RoleSeedData(HaxpeMigrationsDbContext context)
        {
            _context = context;
        }

        public async Task SeedRoles()
        {
            var roles = new string[] { RoleConstants.Admin, RoleConstants.Partner, RoleConstants.Worker, RoleConstants.Customer };

            var roleStore = new RoleStore<IdentityRole>(_context);

            foreach (var role in roles)
            {
                if (!_context.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole { Name = role, NormalizedName = role.ToUpper() });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
