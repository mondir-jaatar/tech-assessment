using WeChooz.TechAssessment.Domain.Common;

namespace WeChooz.TechAssessment.Domain.Entities;

public class Participant : DeletableBaseEntityWithId
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string CompanyName { get; set; }
}