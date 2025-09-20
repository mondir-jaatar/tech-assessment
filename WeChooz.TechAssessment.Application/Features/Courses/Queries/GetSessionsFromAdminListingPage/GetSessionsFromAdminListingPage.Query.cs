using MediatR;
using WeChooz.TechAssessment.Application.Features.Courses.Queries.GetSessionsFromPublicListingPage;
using WeChooz.TechAssessment.Application.Wrappers;
using WeChooz.TechAssessment.Domain.Enums;

namespace WeChooz.TechAssessment.Application.Features.Courses.Queries.GetSessionsFromAdminListingPage;

public class GetSessionsFromAdminListingPageQuery : IRequest<Response<IEnumerable<SessionFromAdminListingPageDto>>>
{
}

public record SessionFromAdminListingPageDto
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public CourseFromAdminListingPageDto Course { get; set; }
    public DeliveryMode DeliveryMode { get; set; }
    public int Duration { get; set; }
    public int RemainingSeats { get; set; }
    public TrainerFromAdminListingPageDto Trainer { get; set; }
}

public record CourseFromAdminListingPageDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CourseDescriptionFromAdminListingPageDto Description { get; set; }
    public TargetAudience TargetAudience { get; set; }
    public int MaxParticipants { get; set; }
}

public record CourseDescriptionFromAdminListingPageDto
{
    public string Short { get; set; }
    public string Long { get; set; }
}

public record TrainerFromAdminListingPageDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }
}