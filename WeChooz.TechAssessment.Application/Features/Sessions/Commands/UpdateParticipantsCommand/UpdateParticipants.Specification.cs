using Ardalis.Specification;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateParticipantsCommand;

public class UpdateParticipantsSpecification : Specification<Session>, ISingleResultSpecification<Session>
{
    public UpdateParticipantsSpecification(Guid sessionId)
    {
        Query.AsNoTracking();
        
        Query.Where(s =>  s.Id == sessionId);

        Query.Include(s => s.Participants);
    }
}