using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Skinet.API.Extensions
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedisCacheServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<ConnectionMultiplexer>(options =>
            {
                var configuration = ConfigurationOptions.Parse(config.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            return services;
        }
    }
}
