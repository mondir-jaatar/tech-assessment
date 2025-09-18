using Ardalis.Specification;
using WeChooz.TechAssessment.Domain.Entities;
using WeChooz.TechAssessment.Domain.Enums;

namespace WeChooz.TechAssessment.Application.Features.Courses.Queries.GetSessionsFromPublicListingPage;

public class GetSessionsFromPublicListingPageCriteriaSpecification : Specification<Session>
{
    public GetSessionsFromPublicListingPageCriteriaSpecification(
        TargetAudience? targetAudience,
        DeliveryMode? deliveryMode,
        DateTime? startDate,
        DateTime? endDate)
    {
        Query.AsNoTracking();

        Query.Include(s => s.Course);

        if (targetAudience.HasValue)
        {
            Query.Where(s => s.Course.TargetAudience == targetAudience.Value);
        }

        if (deliveryMode.HasValue)
        {
            Query.Where(s => s.DeliveryMode == deliveryMode.Value);
        }

        if (startDate.HasValue)
        {
            Query.Where(s => s.StartDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            Query.Where(s => s.StartDate.AddDays(s.Course.Duration) <= endDate.Value);
        }
    }
}

public static class GetSessionsFromPublicListingPageCriteriaSpecificationExtensions
{
    public static Specification<Session, SessionsFromPublicListingPageDto> Project(this GetSessionsFromPublicListingPageCriteriaSpecification spec, int pageNumber, int pageSize)
    {
        var projectSpec = new GetSessionsFromPublicListingPageProjectSpecification(pageNumber, pageSize);
        spec.WithProjectionOf(projectSpec);

        return projectSpec;
    }
}

public class GetSessionsFromPublicListingPageProjectSpecification : Specification<Session, SessionsFromPublicListingPageDto>
{
    public GetSessionsFromPublicListingPageProjectSpecification(int pageNumber, int pageSize)
    {
        Query.Include(s => s.Course.Trainer);

        Query.Select(s => new SessionsFromPublicListingPageDto
        {
            Id = s.Id,
            StartDate = s.StartDate,
            Duration = s.Duration,
            DeliveryMode = s.DeliveryMode,
            Course = new()
            {
                Id = s.Course.Id,
                Name = s.Course.Name,
                TargetAudience = s.Course.TargetAudience,
                Description = new()
                {
                    Long = s.Course.Description.Long,
                    Short = s.Course.Description.Short,
                },
                MaxParticipants = s.Course.MaxParticipants
            },
            NumberOfParticipants = s.Participants.Count,
            Trainer = new()
            {
                FirstName = s.Course.Trainer.FirstName,
                LastName = s.Course.Trainer.LastName,
            },
        });

        Query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}