using Ardalis.Specification;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateParticipantsCommand;

public class UpdateParticipantsCommandSpecification : Specification<Session>, ISingleResultSpecification<Session>
{
    public UpdateParticipantsCommandSpecification(Guid sessionId)
    {
        Query.AsNoTracking();
        
        Query.Where(s =>  s.Id == sessionId);

        Query.Include(s => s.Participants);
    }
}