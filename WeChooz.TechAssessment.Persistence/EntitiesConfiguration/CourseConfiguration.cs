using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Persistence.EntitiesConfiguration;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        // Name
        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        // Description
        builder.OwnsOne(p => p.Description, navigationBuilder =>
        {
            navigationBuilder.Property(p => p.Short)
                .HasMaxLength(255)
                .IsRequired();
            
            navigationBuilder.Property(p => p.Long)
                .IsRequired();
        });

        // Duration
        builder.Property(c => c.Duration)
            .IsRequired();

        // Audience
        builder.Property(p => p.Audience)
            .HasConversion<string>();
        
        // MaxCapacity
        builder.Property(p => p.MaxCapacity)
            .IsRequired();
        
        // Trainer
        builder.HasOne(p => p.Trainer)
            .WithMany()
            .HasForeignKey(p => p.TrainerId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Course_Duration_Min", "[Duration] >= 1");
            t.HasCheckConstraint("CK_Course_MaxCapacity_Min", "[MaxCapacity] >= 1");
        });
    }
}