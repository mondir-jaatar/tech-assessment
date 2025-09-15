namespace WeChooz.TechAssessment.Domain.Common;

public interface IVersionedEntity
{
    public byte[] RowVersion { get; set; }
}