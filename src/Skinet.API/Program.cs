using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Skinet.API.Extensions;
using Skinet.Entities.Entities.Identity;
using Skinet.Persistence;
using Skinet.Persistence.Identity;
using System;
using System.Threading.Tasks;

namespace Skinet.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Migrates any pending migrations
            await MigrateStoreContextAsync(host);
            await MigrateIdentityContextAsync(host);

            // Seeds data to storecontext
            SeedStoreContext(host);

            // Seeds data to identitycontext
            SeedIdentityContext(host);

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        private static async Task MigrateStoreContextAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    var context = services.GetRequiredService<StoreContext>();
                    await context.Database.MigrateAsync();
                }
                catch (Exception e)
                {
                    logger.LogError(e, "An error occured while migrating database associated with {DbContext}"
                        , typeof(StoreContext).Name);
                }
            }
        }
        
        private static async Task MigrateIdentityContextAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    var context = services.GetRequiredService<SkinetIdentityDbContext>();
                    await context.Database.MigrateAsync();
                }
                catch (Exception e)
                {
                    logger.LogError(e, "An error occured while migrating database associated with {DbContext}"
                        , typeof(SkinetIdentityDbContext).Name);
                }
            }
        } 

        private static void SeedStoreContext(IHost host)
        {
            host.SeedDatabase<StoreContext>((context, services) =>
            {
                var logger = services.GetRequiredService<ILogger<StoreContextSeed>>();
                StoreContextSeed.SeedAsync(context, logger).Wait();
            });
        }
        private static void SeedIdentityContext(IHost host)
        {
            host.SeedDatabase<SkinetIdentityDbContext>((context, services) =>
            {
                var logger = services.GetRequiredService<ILogger<SkinetIdentityUserSeed>>();
                var skinetUser = services.GetRequiredService<UserManager<SkinetUser>>();
                SkinetIdentityUserSeed.SeedAsync(skinetUser, logger).Wait(); 
            });
        }
    }
}
