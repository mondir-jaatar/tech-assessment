using Ardalis.Specification;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Queries.GetSessionParticipants;

public class GetSessionParticipantsSpecification : Specification<Participant, SessionParticipantFromSessionAdminDto>
{
    public GetSessionParticipantsSpecification(Guid sessionId)
    {
        Query.AsNoTracking();
        
        Query.Where(x => x.SessionId == sessionId);
        
        Query.Select(participant => new(participant.Id, participant.FirstName, participant.LastName, participant.Email, participant.CompanyName));
    }
}