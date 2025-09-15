using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeChooz.TechAssessment.Application.Interfaces;
using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Domain.Entities;
using WeChooz.TechAssessment.Persistence.Repositories;

namespace WeChooz.TechAssessment.Persistence;

public static class ServiceExtensions
{
    public static void AddPersistenceInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        bool useLazyLoading = true,
        Action? addServices = null)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            Console.WriteLine("******************************");
            Console.WriteLine("Adding InMemory Database");
            Console.WriteLine("******************************");
            
            services.AddDbContext<CourseDbContext>(options => options.UseInMemoryDatabase("Course"));
        }
        else
        {
            services.AddDbContext<CourseDbContext>((s, o) =>
            {
                var connectionString = configuration.GetConnectionString("Course");
                
                Console.WriteLine("===============================");
                Console.WriteLine($"Using connection string: {connectionString}");
                Console.WriteLine("===============================");

                if (useLazyLoading)
                    o.UseLazyLoadingProxies().UseSqlServer(connectionString);
                else
                    o.UseSqlServer(connectionString).UseSqlServer(connectionString);
            });
        }

        services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICourseRepositoryAsync, CourseRepositoryAsync>();
        services.AddScoped<ISessionRepositoryAsync, SessionRepositoryAsync>();
        services.AddScoped<ITrainerRepositoryAsync, TrainerRepositoryAsync>();
        addServices?.Invoke();
    }
}
