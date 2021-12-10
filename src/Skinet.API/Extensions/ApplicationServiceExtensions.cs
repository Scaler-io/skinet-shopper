using Microsoft.Extensions.DependencyInjection;

namespace Skinet.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // api versioning settings
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = Microsoft.AspNetCore.Mvc.ApiVersion.Default;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
            return services;
        }
    }
}
