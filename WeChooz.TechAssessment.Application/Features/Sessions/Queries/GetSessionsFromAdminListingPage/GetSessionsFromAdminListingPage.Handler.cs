using MediatR;
using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Application.Wrappers;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Queries.GetSessionsFromAdminListingPage;

public class GetSessionsFromAdminListingPageHandler(ISessionRepositoryAsync sessionRepository) : IRequestHandler<GetSessionsFromAdminListingPageQuery, Response<IEnumerable<SessionFromAdminListingPageDto>>>
{
    public async Task<Response<IEnumerable<SessionFromAdminListingPageDto>>> Handle(GetSessionsFromAdminListingPageQuery request, CancellationToken cancellationToken)
    {
        var specification = new GetSessionsFromAdminListingPageSpecification();
        
        var sessions = await sessionRepository.GetBySpecificationAsync(specification, cancellationToken);

        return new Response<IEnumerable<SessionFromAdminListingPageDto>>(sessions);
    }
}