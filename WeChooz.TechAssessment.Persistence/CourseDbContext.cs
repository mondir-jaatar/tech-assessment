using Microsoft.EntityFrameworkCore;
using WeChooz.TechAssessment.Application.Interfaces.Services;
using WeChooz.TechAssessment.Domain.Common;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Persistence;

public class CourseDbContext(IDateTimeService dateTimeService, IAuthenticatedUserService authenticatedUserService) : DbContext
{
    public DbSet<Trainer> Trainers { get; set; }
    
    public DbSet<Course> Course { get; set; }

    public DbSet<Session> Sessions { get; set; }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>().ToArray())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = dateTimeService.NowUtc;
                    entry.Entity.CreatedBy = authenticatedUserService.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModified = dateTimeService.NowUtc;
                    entry.Entity.LastModifiedBy = authenticatedUserService.UserId;
                    break;
                case EntityState.Deleted:
                    if (entry.Entity is SolftDeletableBaseEntityWithId entity)
                    {
                        entry.State = EntityState.Modified;
                        entity.IsDeleted = true;
                        entity.Deleted = dateTimeService.NowUtc;
                    }

                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureDefaultIds(builder);
        ConfigureRowVersion(builder);
        ConfigureSoftDelete(builder);
    }
    
    private static void ConfigureDefaultIds(ModelBuilder builder)
    {
        var entityTypes = builder.Model.GetEntityTypes()
            .Where(e => typeof(AuditableBaseEntityWithId).IsAssignableFrom(e.ClrType));

        foreach (var entityType in entityTypes)
        {
            builder.Entity(entityType.ClrType)
                .Property("Id")
                .HasDefaultValueSql("newsequentialid()");
        }
    }

    private static void ConfigureRowVersion(ModelBuilder builder)
    {
        var entityTypes = builder.Model.GetEntityTypes()
            .Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType));

        foreach (var entityType in entityTypes)
        {
            builder.Entity(entityType.ClrType)
                .Property("RowVersion")
                .IsRowVersion();
        }
    }

    private static void ConfigureSoftDelete(ModelBuilder builder)
    {
        var deletableTypes = builder.Model.GetEntityTypes()
            .Where(e => typeof(SolftDeletableBaseEntityWithId).IsAssignableFrom(e.ClrType));

        foreach (var entityType in deletableTypes)
        {
            entityType.AddSoftDeleteQueryFilter();
        }
    }
}