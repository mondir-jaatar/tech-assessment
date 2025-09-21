using MediatR;
using WeChooz.TechAssessment.Application.Wrappers;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Queries.GetSessionParticipants;

public class GetSessionParticipantsFromAdminPageQuery : IRequest<Response<IEnumerable<SessionParticipantFromSessionAdminDto>>>
{
    public Guid SessionId { get; set; }
}

public record SessionParticipantFromSessionAdminDto(Guid Id, string FirstName, string LastName, string Email, string CompanyName);