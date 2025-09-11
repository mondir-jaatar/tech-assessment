using Microsoft.Extensions.DependencyInjection;
using WeChooz.TechAssessment.Application.Interfaces.Services;

namespace WeChooz.TechAssessment.Persistence.Shared;

public static class ServiceRegistration
{
    public static void AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeService, DateTimeService>();
    }
}