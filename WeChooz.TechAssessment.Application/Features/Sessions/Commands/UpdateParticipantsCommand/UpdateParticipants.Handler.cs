using MediatR;
using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Application.Wrappers;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateParticipantsCommand;

public class UpdateParticipantsHandler(ISessionRepositoryAsync sessionRepository, IGenericRepositoryAsync<Participant> participantRepository) : IRequestHandler<UpdateParticipantsCommand, Response<IEnumerable<Guid>>>
{
    public async Task<Response<IEnumerable<Guid>>> Handle(UpdateParticipantsCommand request, CancellationToken cancellationToken)
    {
        var specification = new UpdateParticipantsSpecification(request.SessionId);
        var session = await sessionRepository.FirstOrDefaultAsync(specification, cancellationToken);
        var existingParticipants = session.Participants;
        
        DeleteRemovedParticipants(request, existingParticipants, session);
        
        UpdateExistingParticipants(request, existingParticipants);

        AddNewParticipants(request, existingParticipants, session);

        sessionRepository.Update(session);
        
        await sessionRepository.SaveChangesAsync(cancellationToken);

        var allIds = session.Participants.Select(p => p.Id);
        return new(allIds);
    }

    private static void AddNewParticipants(UpdateParticipantsCommand request, ICollection<Participant> existingParticipants, Session session)
    {
        var participantsToAdd = request.Participants
            .Where(rp => existingParticipants.All(ep => ep.Id != rp.Id))
            .Select(rp => rp.ToParticipant(session.Id))
            .ToList();

        session.Participants.AddRange(participantsToAdd);
    }

    private static void UpdateExistingParticipants(UpdateParticipantsCommand request, ICollection<Participant> existingParticipants)
    {
        var participantsToUpdate = existingParticipants
            .Where(ep => request.Participants.Any(rp => rp.Id == ep.Id))
            .ToList();

        foreach (var participant in participantsToUpdate)
        {
            var dto = request.Participants.First(rp => rp.Id == participant.Id);
            dto.ToParticipant(participant);
        }
    }

    private void DeleteRemovedParticipants(UpdateParticipantsCommand request, ICollection<Participant> existingParticipants, Session session)
    {
        var participantsToDelete = existingParticipants
            .Where(ep => request.Participants.All(rp => rp.Id != ep.Id))
            .ToList();

        foreach (var participant in participantsToDelete)
        {
            session.Participants.Remove(participant);
            participantRepository.Delete(participant);
        }
    }
}