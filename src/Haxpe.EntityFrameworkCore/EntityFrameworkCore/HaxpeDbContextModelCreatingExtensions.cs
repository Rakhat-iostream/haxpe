using Haxpe.Addresses;
using Haxpe.Coupons;
using Haxpe.Customers;
using Haxpe.Industries;
using Haxpe.Orders;
using Haxpe.Partners;
using Haxpe.ServiceTypes;
using Haxpe.Users;
using Haxpe.WorkerLocationTrackers;
using Haxpe.Workers;
using Microsoft.EntityFrameworkCore;

namespace Haxpe.EntityFrameworkCore
{
    public static class HaxpeDbContextModelCreatingExtensions
    {
        public static void ConfigureHaxpe(this ModelBuilder builder)
        {
            builder.Entity<User>(b =>
            {
                b.Property(x => x.Surname);
                b.Property(x => x.FullName);
                b.Property(x => x.PartnerId);
                b.Property(x => x.PreferLanguage).HasMaxLength(6);
                b.Property(x => x.IsExternal);

                b.HasIndex(x => x.PartnerId);
            });

            builder.Entity<Partner>(b =>
            {
                b.ToTable(HaxpeConsts.DbTablePrefix + "Partners", HaxpeConsts.DbSchema);
                b.Property(p => p.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
                b.Property(x => x.OwnerUserId).IsRequired();
                b.HasIndex(x => new { x.OwnerUserId });
            });

            builder.Entity<Partner>(b =>
            {
                b.ToTable(HaxpeConsts.DbTablePrefix + "Partners", HaxpeConsts.DbSchema);
                b.Property(p => p.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
                b.Property(x => x.OwnerUserId).IsRequired();
                b.HasIndex(x => new { x.OwnerUserId });
            });

            builder.Entity<Industry>(b =>
            {
                b.ToTable(HaxpeConsts.DbTablePrefix + "Industries", HaxpeConsts.DbSchema);
                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(x => x.Key).IsRequired().HasMaxLength(128);
            });
            
            builder.Entity<ServiceType>(b =>
            {
                b.ToTable(HaxpeConsts.DbTablePrefix + "ServiceTypes", HaxpeConsts.DbSchema);
                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(x => x.Key).IsRequired().HasMaxLength(128);
            });
            
            builder.Entity<Worker>(b =>
            {
                b.ToTable(HaxpeConsts.DbTablePrefix + "Workers", HaxpeConsts.DbSchema);
                b.Property(p => p.Id);
                b.HasIndex(x => new { x.PartnerId, x.UserId }).IsUnique();
                b.HasMany(x => x.ServiceTypes).WithOne();
            });
            
            builder.Entity<WorkerServiceType>(b =>
            {
                b.ToTable(HaxpeConsts.DbTablePrefix + "WorkerServiceTypes", HaxpeConsts.DbSchema);
                b.HasKey(x => new {x.WorkerId, x.ServiceTypeId});
            });
            
            builder.Entity<Customer>(b =>
            {
                b.ToTable(HaxpeConsts.DbTablePrefix + "Customers", HaxpeConsts.DbSchema);
                b.Property(p => p.Id);
                b.Property(p => p.UserId);
                b.Property(p => p.AddressId);
                b.HasIndex(x => new { x.UserId }).IsUnique();
            });
            
            builder.Entity<Address>(b =>
            {
                b.ToTable(HaxpeConsts.DbTablePrefix + "Addresses", HaxpeConsts.DbSchema);
                b.Property(p => p.Id);
                b.Property(p => p.City);
                b.Property(p => p.Country);
                b.Property(p => p.Street);
                b.Property(p => p.BuildingNum);
                b.Property(p => p.ZipCode); 
                b.Property(p => p.Lat);
                b.Property(p => p.Lon);
                b.Property(p => p.ExternalId);
                b.HasIndex(x => new {x.City, x.Street});
                b.HasIndex(x => new { x.ZipCode });
                b.HasIndex(x => new { x.ExternalId });
            });
            
             builder.Entity<Order>(b =>
             {
                 b.ToTable(HaxpeConsts.DbTablePrefix + "Orders", HaxpeConsts.DbSchema);
                 b.Property(p => p.Id);
                 b.Property(p => p.CustomerId);
                 b.Property(p => p.AddressId);
                 b.Property(p => p.PartnerId);
                 b.Property(p => p.WorkerId);
                 b.Property(p => p.IndustryId);
                 b.Property(p => p.ServiceTypeId);
                 b.Property(p => p.OrderDate);
                 b.Property(p => p.StartDate);
                 b.Property(p => p.CompletedDate);
                 b.Property(p => p.PaymentMethod);
                 b.Property(p => p.NetAmount);
                 b.Property(p => p.Tax);
                 b.Property(p => p.TotalAmount);
                 b.Property(p => p.OrderStatus);
                 b.Property(p => p.Rating);
                 b.Property(p => p.Comment);
                 b.Property(p => p.CouponId);
                 b.Property(p => p.CouponCode).HasMaxLength(256);

                 b.HasIndex(x => new { x.CustomerId });
                 b.HasIndex(x => new { x.WorkerId });
                 b.HasIndex(x => new { x.PartnerId });
             });

            builder.Entity<Coupon>(b =>
            {
                b.ToTable(HaxpeConsts.DbTablePrefix + "Coupon", HaxpeConsts.DbSchema);
                b.Property(p => p.Id);
                b.Property(p => p.Code).HasMaxLength(256);
                b.Property(p => p.ExpirationDate);
                b.Property(p => p.CreationDate);
                b.Property(p => p.IsDeleted);
                b.Property(p => p.Value);
                b.Property(p => p.Unit);
                b.HasIndex(x => x.Code);
            });

            builder.Entity<WorkerLocationTracker>(b =>
            {
                b.ToTable(HaxpeConsts.DbTablePrefix + "WorkerLocationTracker", HaxpeConsts.DbSchema);
                b.Property(p => p.Id);
                b.Property(p => p.WorkerId);
                b.Property(p => p.UpdateDate);
                b.Property(p => p.Longitude);
                b.Property(p => p.Latitude);
                b.HasIndex(x => x.WorkerId);
            });
        }
    }
}