using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using WeChooz.TechAssessment.Application.Interfaces;
using WeChooz.TechAssessment.Application.Interfaces.Services;

namespace WeChooz.TechAssessment.Persistence.Shared;

public static class ServiceExtensions
{
    public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeService, DateTimeService>();
        
        AddRedis(services, configuration);
    }
    
    private static void AddRedis(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var redisServer = configuration.GetValue<string>("redis:server");
            var redisPort = configuration.GetValue<int>("redis:port");
            var redisUser = configuration.GetValue<string>("redis:user");
            var redisPassword = configuration.GetValue<string>("redis:password");
            
            var options = new ConfigurationOptions
            {
                EndPoints = { $"{redisServer}:{redisPort}" },
                User = redisUser,
                Password = redisPassword,
                Ssl = false,
                AbortOnConnectFail = false
            };
        
            var connectionMultiplexer = ConnectionMultiplexer.Connect(options);

            return connectionMultiplexer;
        });
        
        services.AddTransient<IRedisLockService, RedisLockService>();
    }
}