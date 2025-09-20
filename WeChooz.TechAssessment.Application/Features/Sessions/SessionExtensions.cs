using WeChooz.TechAssessment.Application.Features.Sessions.Commands;
using WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateParticipantsCommand;
using WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateSessionCommand;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Application.Features.Sessions;

public static class SessionExtensions
{
    public static Session ToSession(this SessionCommandBase request, Session session)
    {
        session.StartDate = request.StartDate;
        session.DeliveryMode = request.DeliveryMode;
        session.Duration = request.Duration;
        session.CourseId = request.CourseId;
        
        if (request is UpdateSessionCommand updateCommand)
        {
            session.RowVersion = updateCommand.RowVersion;    
        }

        return session;
    }
    
    public static Participant ToParticipant(this UpdateParticipantDto request, Participant paricipant)
    {
        paricipant.FirstName = request.FirstName;
        paricipant.LastName = request.LastName;
        paricipant.CompanyName = request.CompanyName;
        paricipant.Email = request.Email;

        return paricipant;
    }
    
    public static Participant ToParticipant(this UpdateParticipantDto request, Guid sessionId)
    {
        var participant = new Participant()
        {
            SessionId = sessionId
        };
        request.ToParticipant(participant);

        return participant;
    }

    public static SessionAfterUpdateDto Map(this Session session)
    {
        return new()
        {
            Id = session.Id,
            RowVersion = session.RowVersion
        };
    }
    
    public static Session ToSession(this SessionCommandBase request)
    {
        var session = new Session()
        {
            StartDate = request.StartDate,
            DeliveryMode = request.DeliveryMode,
            Duration = request.Duration,
            CourseId = request.CourseId,
        };

        return session;
    }
}