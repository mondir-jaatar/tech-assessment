using Ardalis.Specification;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateSessionCommand;

public class UpdateSessionSpecification : Specification<Session>, ISingleResultSpecification<Session>
{
    public UpdateSessionSpecification(Guid sessionId)
    {
        Query.AsNoTracking();
        Query.Where(s => s.Id == sessionId);
    }
}