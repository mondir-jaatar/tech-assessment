using WeChooz.TechAssessment.Domain.Common;

namespace WeChooz.TechAssessment.Domain.Entities;

public class Trainer : SolftDeletableBaseEntityWithId
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}