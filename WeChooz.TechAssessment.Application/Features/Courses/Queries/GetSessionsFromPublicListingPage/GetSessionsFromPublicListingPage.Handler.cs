using MediatR;
using WeChooz.TechAssessment.Application.Interfaces.Repositories;

namespace WeChooz.TechAssessment.Application.Features.Courses.Queries.GetSessionsFromPublicListingPage;

public class GetSessionsFromPublicListingPageHandler(ISessionRepositoryAsync sessionRepository) : IRequestHandler<GetSessionsFromPublicListingPageQuery, SessionsFromPublicListingPageViewModel>
{
    public async Task<SessionsFromPublicListingPageViewModel> Handle(GetSessionsFromPublicListingPageQuery request, CancellationToken cancellationToken)
    {
        var filterSpecification = new GetSessionsFromPublicListingPageCriteriaSpecification(request.TargetAudience, request.DeliveryMode, request.StartDate, request.EndDate);
        var projectSpecification = filterSpecification.Project(request.PageNumber, request.PageSize);
        
        var count = await sessionRepository.CountAsync(filterSpecification, cancellationToken);
        var sessions = await sessionRepository.GetBySpecificationAsync(projectSpecification, cancellationToken);
        
        // Calculating RemainingSeats to eliminate 1 count subquery, a better idea is to calculate save RemainingSeats when session is saved, TODO for later (maybe)
        sessions = sessions.Select(s => s with { RemainingSeats = s.Course.MaxParticipants - s.NumberOfParticipants }).ToList();

        return new(sessions,request.PageNumber, request.PageSize, count);
    }
}