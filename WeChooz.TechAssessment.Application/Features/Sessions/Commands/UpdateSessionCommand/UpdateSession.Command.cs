using MediatR;
using WeChooz.TechAssessment.Application.Wrappers;
using WeChooz.TechAssessment.Domain.Enums;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateSessionCommand;

public class UpdateSessionCommand : SessionCommandBase, IRequest<Response<SessionAfterUpdateDto>>
{
    public Guid Id { get; set; }
    
    public byte[] RowVersion { get; set; }
}

public record SessionAfterUpdateDto
{
    public Guid Id { get; set; }
    
    public byte[] RowVersion { get; set; }
}