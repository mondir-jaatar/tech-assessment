using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Persistence.EntitiesConfiguration;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        // Course
        
        builder.HasOne(p => p.Course)
            .WithMany()
            .HasForeignKey(p => p.CourseId);
    }
}