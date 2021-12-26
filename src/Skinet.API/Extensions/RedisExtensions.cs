using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Skinet.API.Extensions
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedisCacheServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var configuration = ConfigurationOptions.Parse(config.GetConnectionString("BasketDb"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            return services;
        }
    }
}
