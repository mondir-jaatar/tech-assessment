using WeChooz.TechAssessment.Application.Exceptions;

namespace WeChooz.TechAssessment.Application.Wrappers;

public class ValidationErrorResponse : Response<IEnumerable<ValidationErrorAsKeyValue>>
{
    public bool IsValidationError { get; set; }

    public ValidationErrorResponse()
    {
        IsValidationError = true;
    }
}