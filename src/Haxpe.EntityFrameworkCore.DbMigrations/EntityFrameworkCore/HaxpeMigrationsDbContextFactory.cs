using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Haxpe.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class HaxpeMigrationsDbContextFactory : IDesignTimeDbContextFactory<HaxpeMigrationsDbContext>
    {
        public HaxpeMigrationsDbContext CreateDbContext(string[] args)
        {

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<HaxpeMigrationsDbContext>()
                .UseNpgsql(configuration.GetConnectionString("Default"));

            return new HaxpeMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Haxpe.DbMigrator/"))
                .AddJsonFile("appsettings.Development.json", optional: false);

            return builder.Build();
        }
    }
}
