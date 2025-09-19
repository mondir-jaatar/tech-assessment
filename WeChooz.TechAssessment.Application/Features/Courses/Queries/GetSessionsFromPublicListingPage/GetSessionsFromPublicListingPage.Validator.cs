using FluentValidation;

namespace WeChooz.TechAssessment.Application.Features.Courses.Queries.GetSessionsFromPublicListingPage;

public class GetSessionsFromPublicListingPageValidator : AbstractValidator<GetSessionsFromPublicListingPageQuery>
{
    public GetSessionsFromPublicListingPageValidator()
    {
        RuleFor(p => p.StartDate)
            .LessThanOrEqualTo(p => p.EndDate)
            .WithMessage("La date de début doit être avant la date de fin");
        
        RuleFor(p => p.EndDate)
            .GreaterThanOrEqualTo(p => p.StartDate)
            .WithMessage("La date de fin doit être après la date de début");
    }
}