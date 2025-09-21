using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Persistence.EntitiesConfiguration;

public class ParticipantConfiguration: IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.Property(p => p.FirstName)
            .HasMaxLength(100);
        
        builder.Property(p => p.LastName)
            .HasMaxLength(100);
        
        builder.Property(p => p.Email)
            .HasMaxLength(100);
        
        builder.Property(p => p.CompanyName)
            .HasMaxLength(100);
        
        //Session
        builder.HasOne(p => p.Session)
            .WithMany(s => s.Participants)
            .HasForeignKey(p => p.SessionId);
    }
}