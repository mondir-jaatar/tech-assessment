using WeChooz.TechAssessment.Domain.Common;
using WeChooz.TechAssessment.Domain.Enums;

namespace WeChooz.TechAssessment.Domain.Entities;

public class Session : SolftDeletableBaseEntityWithId, IVersionedEntity
{
    public required DateTime StartDate { get; set; }

    // public required DateTime EndDate { get; set; }
    
    public required DeliveryMode DeliveryMode { get; set; }

    public required int Duration { get; set; }
    
    public virtual Course Course { get; set; }

    public required Guid CourseId { get; set; }

    public virtual ICollection<Participant> Participants { get; set; }
    
    public byte[] RowVersion { get; set; }
}