using Ardalis.Specification;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Application.Features.Courses.Queries.GetSessionsFromAdminListingPage;

public class GetSessionsFromAdminListingPageSpecification: Specification<Session, SessionFromAdminListingPageDto>
{
    public GetSessionsFromAdminListingPageSpecification()
    {
        Query.Include(s => s.Course);
        Query.Include(s => s.Course.Trainer);

        Query.Select(s => new SessionFromAdminListingPageDto
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
            Trainer = new()
            {
                Id = s.Course.Trainer.Id,
                FirstName = s.Course.Trainer.FirstName,
                LastName = s.Course.Trainer.LastName,
            },
        });
    }
}