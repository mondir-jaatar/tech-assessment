using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Persistence.EntitiesConfiguration;

public class ParticipantConfiguration: IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        //Session
        builder.HasOne(p => p.Session)
            .WithMany()
            .HasForeignKey(p => p.SessionId);
    }
}