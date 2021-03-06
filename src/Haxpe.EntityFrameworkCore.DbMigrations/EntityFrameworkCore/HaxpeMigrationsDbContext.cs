using System;
using Haxpe.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Haxpe.EntityFrameworkCore
{
    public class HaxpeMigrationsDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public HaxpeMigrationsDbContext(DbContextOptions<HaxpeMigrationsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureHaxpe();
        }
    }
}