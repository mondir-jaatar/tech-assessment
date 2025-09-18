using WeChooz.TechAssessment.Domain.Common;
using WeChooz.TechAssessment.Domain.Enums;

namespace WeChooz.TechAssessment.Domain.Entities;

public class Session : SolftDeletableBaseEntityWithId, IVersionedEntity
{
    public DateTime StartDate { get; set; }
    
    public DeliveryMode DeliveryMode { get; set; }

    public int Duration { get; set; }
    
    public virtual Course Course { get; set; }

    public Guid CourseId { get; set; }

    public virtual ICollection<Participant> Participants { get; set; }
    
    public byte[] RowVersion { get; set; }
}