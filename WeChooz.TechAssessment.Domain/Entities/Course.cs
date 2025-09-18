using WeChooz.TechAssessment.Domain.Common;
using WeChooz.TechAssessment.Domain.Enums;

namespace WeChooz.TechAssessment.Domain.Entities;

public class Course : SolftDeletableBaseEntityWithId, IVersionedEntity
{
    public string Name { get; set; }

    public Description Description { get; set; }
    
    public int Duration { get; set; }
    
    public TargetAudience TargetAudience { get; set; }
    
    public int MaxParticipants { get; set; }
    
    public virtual Trainer Trainer { get; set; }

    public Guid? TrainerId { get; set; }
    
    public byte[] RowVersion { get; set; }
}