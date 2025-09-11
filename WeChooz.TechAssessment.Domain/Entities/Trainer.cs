using WeChooz.TechAssessment.Domain.Common;

namespace WeChooz.TechAssessment.Domain.Entities;

public class Trainer : DeletableBaseEntityWithId
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}