using MediatR;
using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Application.Wrappers;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Queries.GetSessionParticipants;

public class GetSessionParticipantsHandler(IGenericRepositoryAsync<Participant> participantRepository) : IRequestHandler<GetSessionParticipantsFromAdminPageQuery, Response<IEnumerable<SessionParticipantFromSessionAdminDto>>>
{
    public async Task<Response<IEnumerable<SessionParticipantFromSessionAdminDto>>> Handle(GetSessionParticipantsFromAdminPageQuery request, CancellationToken cancellationToken)
    {
        var specification = new GetSessionParticipantsSpecification(request.SessionId);

        var participants = await participantRepository.GetBySpecificationAsync(specification, cancellationToken);
        
        return new(participants);
    }
}