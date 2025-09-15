namespace WeChooz.TechAssessment.Application.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException() : base("Element not found")
    {
    }
}