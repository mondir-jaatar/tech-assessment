using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeChooz.TechAssessment.Application.Interfaces;
using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Persistence.Repositories;

namespace WeChooz.TechAssessment.Persistence;

public static class ServiceExtensions
{
    public static void AddPersistenceInfrastructure<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        bool useLazyLoading = true,
        Action? addServices = null) where TContext : DbContext
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<TContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
        }
        else
        {
            services.AddDbContext<TContext>((s, o) =>
            {
                var connectionString = configuration.GetConnectionString("Course");

                if (useLazyLoading)
                    o.UseLazyLoadingProxies().UseSqlServer(connectionString);
                else
                    o.UseSqlServer(connectionString);
            });
        }

        services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
        services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
        services.AddScoped<ICourseRepositoryAsync, CourseRepositoryAsync>();
        services.AddScoped<ISessionRepositoryAsync, SessionRepositoryAsync>();
        services.AddScoped<ITrainerRepositoryAsync, TrainerRepositoryAsync>();
        addServices?.Invoke();
    }
}
