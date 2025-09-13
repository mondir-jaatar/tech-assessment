using WeChooz.TechAssessment.Domain.Common;
using WeChooz.TechAssessment.Domain.Enums;

namespace WeChooz.TechAssessment.Domain.Entities;

public class Session : AuditableBaseEntityWithId
{
    public DateTime StartDate { get; set; }
    
    public DeliveryMode DeliveryMode { get; set; }
    
    public virtual Course Course { get; set; }

    public Guid CourseId { get; set; }
}