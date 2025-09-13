namespace WeChooz.TechAssessment.Domain.Common;

public abstract class SolftDeletableBaseEntityWithId : AuditableBaseEntityWithId
{
    public bool IsDeleted { get; set; }
    
    public DateTime? Deleted { get; set; }
    
    public Guid? DeletedBy { get; set; }
}