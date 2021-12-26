using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Skinet.API.Extensions;
using Skinet.Persistence;
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

            // seeds data to database
            host.SeedDatabase<StoreContext>((context, services) =>
            {
                var logger = services.GetRequiredService<ILogger<StoreContextSeed>>();
                StoreContextSeed.SeedAsync(context, logger).Wait();
            });

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
