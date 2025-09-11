using WeChooz.TechAssessment.Domain.Common;
using WeChooz.TechAssessment.Domain.Enums;

namespace WeChooz.TechAssessment.Domain.Entities;

public class Session : AuditableBaseEntityWithId
{
    public Course Course { get; set; }

    public Guid CourseId { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DeliveryMode DeliveryMode { get; set; }
    
    public List<Participant> Participants { get; set; }
}