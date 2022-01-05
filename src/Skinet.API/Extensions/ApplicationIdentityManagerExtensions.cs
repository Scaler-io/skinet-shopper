using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skinet.Persistence.Identity;


namespace Skinet.API.Extensions
{
    public static class ApplicationIdentityManagerExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration configuration    
        )
        {
            services.AddDbContext<SkinetIdentityDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("IdentityConnection"));
            });
            return services;
        }
    }
}
