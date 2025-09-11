namespace WeChooz.TechAssessment.Domain.Common;

public abstract class AuditableBaseEntityWithId : AuditableBaseEntity
{
    public Guid Id { get; set; }
}