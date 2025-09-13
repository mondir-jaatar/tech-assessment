using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeChooz.TechAssessment.Domain.Common;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Persistence.EntitiesConfiguration;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        // FirstName
        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        
        // LastName
        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(50);
    }
}