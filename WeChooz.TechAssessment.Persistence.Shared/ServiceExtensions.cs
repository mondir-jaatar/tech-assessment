using Microsoft.Extensions.DependencyInjection;
using WeChooz.TechAssessment.Application.Interfaces;
using WeChooz.TechAssessment.Application.Interfaces.Services;

namespace WeChooz.TechAssessment.Persistence.Shared;

public static class ServiceExtensions
{
    public static void AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddScoped<IRedisLockService, RedisLockService>();
    }
}