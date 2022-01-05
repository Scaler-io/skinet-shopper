using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skinet.Entities.Entities.Identity;
using Skinet.Persistence.Identity;


namespace Skinet.API.Extensions
{
    public static class ApplicationIdentityManagerExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration configuration    
        )
        {
            var builder = services.AddIdentityCore<SkinetUser>();
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<SkinetIdentityDbContext>();
            builder.AddSignInManager<SignInManager<SkinetUser>>();

            services.AddAuthentication();

            return services;
        }
    }
}
