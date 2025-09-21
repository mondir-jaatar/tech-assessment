using MediatR;
using WeChooz.TechAssessment.Application.Wrappers;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateParticipantsCommand;

public class UpdateParticipantsCommand : IRequest<Response<IEnumerable<Guid>>>
{
    public Guid SessionId { get; set; }
    public IEnumerable<UpdateParticipantDto>  Participants { get; set; } = [];
}

public record UpdateParticipantDto
{
    public Guid? Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string CompanyName { get; set; }
}