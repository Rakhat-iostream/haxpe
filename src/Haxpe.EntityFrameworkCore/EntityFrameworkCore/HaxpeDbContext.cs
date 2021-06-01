using Haxpe.Addresses;
using Haxpe.Customers;
using Haxpe.Industries;
using Haxpe.Orders;
using Haxpe.Partners;
using Haxpe.ServiceTypes;
using Microsoft.EntityFrameworkCore;
using Haxpe.Users;
using Haxpe.Workers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

namespace Haxpe.EntityFrameworkCore
{
    public class HaxpeDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Partner> Partners { get; set; }
        
        public DbSet<Worker> Workers { get; set; }
        
        public DbSet<Industry> Industries { get; set; }
        
        public DbSet<ServiceType> ServiceTypes { get; set; }
        
        public DbSet<WorkerServiceType> WorkerServiceTypes { get; set; }
        
        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<Address> Addresses { get; set; }
        
        public DbSet<Order> Orders { get; set; }

        public HaxpeDbContext(DbContextOptions<HaxpeDbContext> options)
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
