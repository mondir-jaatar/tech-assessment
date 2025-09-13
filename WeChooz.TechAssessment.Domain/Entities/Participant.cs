using WeChooz.TechAssessment.Domain.Common;

namespace WeChooz.TechAssessment.Domain.Entities;

public class Participant : AuditableBaseEntityWithId
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string CompanyName { get; set; }

    public virtual Session Session { get; set; }

    public Guid SessionId { get; set; }
}