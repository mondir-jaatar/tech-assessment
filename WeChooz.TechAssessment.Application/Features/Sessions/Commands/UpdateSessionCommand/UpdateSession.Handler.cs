using MediatR;
using Microsoft.EntityFrameworkCore;
using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Application.Wrappers;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateSessionCommand;

public class UpdateSessionHandler(ISessionRepositoryAsync sessionRepository) : IRequestHandler<UpdateSessionCommand,  Response<SessionAfterUpdateDto>>
{
    public async Task<Response<SessionAfterUpdateDto>> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
    {
        var specification = new UpdateSessionSpecification(request.Id);

        var session = await sessionRepository.FirstOrDefaultAsync(specification, cancellationToken);

        sessionRepository.Update(request.ToSession(session));
        
        try
        {
            await sessionRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException e)
        {
           throw new DbUpdateConcurrencyException("La session a été mise à jour par un autre utilisateur");
        }

        return new(session.Map());
    }
}