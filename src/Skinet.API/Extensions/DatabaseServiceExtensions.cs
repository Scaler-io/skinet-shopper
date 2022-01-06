using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skinet.Persistence;
using Skinet.Persistence.Identity;

namespace Skinet.API.Extensions
{
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<SkinetIdentityDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("IdentityConnection"));
            });

            return services;
        }
    }
}
