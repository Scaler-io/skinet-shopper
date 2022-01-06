using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Skinet.Entities.Entities.Identity;
using Skinet.Persistence.Identity;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.API.Extensions
{
    public static class ApplicationIdentityManagerExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration configuration    
        )
        {
            services.AddIdentityCore<SkinetUser>(options => {
                options.Password.RequireNonAlphanumeric = false;
            })
           .AddEntityFrameworkStores<SkinetIdentityDbContext>()
           .AddSignInManager<SignInManager<SkinetUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });
            
            return services;
        }
    }
}
