using WeChooz.TechAssessment.Domain.Enums;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Commands;

public class SessionCommandBase
{
    public required DateTime StartDate { get; set; }

    public required DeliveryMode DeliveryMode { get; set; }

    public required int Duration { get; set; }

    public required Guid CourseId { get; set; }
}