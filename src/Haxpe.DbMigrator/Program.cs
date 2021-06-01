using System;
using System.IO;
using System.Threading.Tasks;
using Haxpe.DbMigrator.Seeds;
using Haxpe.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Haxpe.DbMigrator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<HaxpeMigrationsDbContext>();
                db.Database.Migrate();
                var roleSeed = scope.ServiceProvider.GetRequiredService<RoleSeedData>();
                await roleSeed.SeedRoles();
            }
            Console.WriteLine("Migration is done");
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) => logging.ClearProviders())
                .ConfigureServices((hostContext, services) =>
                {
                    var builder = new ConfigurationBuilder()
                        .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: false);
                    var config = builder.Build();

                    services.AddDbContext<HaxpeMigrationsDbContext>(b => b.UseNpgsql(config.GetConnectionString("Default")));
                    services.AddTransient<RoleSeedData>();
                });
    }
}
