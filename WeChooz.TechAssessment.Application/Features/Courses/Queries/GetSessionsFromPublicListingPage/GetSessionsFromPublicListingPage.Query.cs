using MediatR;
using WeChooz.TechAssessment.Application.Wrappers;
using WeChooz.TechAssessment.Domain.Enums;

namespace WeChooz.TechAssessment.Application.Features.Courses.Queries.GetSessionsFromPublicListingPage;

public class GetSessionsFromPublicListingPageQuery : IRequest<SessionsFromPublicListingPageViewModel>
{
    public TargetAudience? TargetAudience { get; set; }
    public DeliveryMode? DeliveryMode { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageNumber  { get; set; }
    public int PageSize  { get; set; }
}

public record SessionFromPublicListingPageDto
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public CourseFromPublicListingPageDto Course { get; set; }
    public DeliveryMode DeliveryMode { get; set; }
    public int Duration { get; set; }
    public int RemainingSeats { get; set; }
    public TrainerFromPublicListingPageDto Trainer { get; set; }
    public int NumberOfParticipants { get; set; }
}

public record CourseFromPublicListingPageDto
{
    public string Name { get; set; }
    public CourseDescriptionFromPublicListingPageDto Description { get; set; }
    public TargetAudience TargetAudience { get; set; }
    public int MaxParticipants { get; set; }
}

public record CourseDescriptionFromPublicListingPageDto
{
    public string Short { get; set; }
    public string Long { get; set; }
}

public record TrainerFromPublicListingPageDto
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}

public class SessionsFromPublicListingPageViewModel : PagedResponse<ICollection<SessionFromPublicListingPageDto>>
{
    public int Count { get; set; }
    public SessionsFromPublicListingPageViewModel(ICollection<SessionFromPublicListingPageDto> data, int pageNumber, int pageSize, int count) : base(data, pageNumber, pageSize)
    {
        Count = count;
    }
}