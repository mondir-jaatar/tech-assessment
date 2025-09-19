using FluentValidation.Results;

namespace WeChooz.TechAssessment.Application.Exceptions;

public class ValidationException() : Exception("One or more validation failures have occurred.")
{
    public List<ValidationErrorAsKeyValue> Errors { get; } = new();

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures.Select(error => new ValidationErrorAsKeyValue
        {
            Property = error.PropertyName.CamelCase(),
            Message = error.ErrorMessage
        }).ToList();
    }
}

public record ValidationErrorAsKeyValue
{
    public string Property { get; set; }
    public string Message { get; set; }
}