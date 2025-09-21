using FluentValidation;
using WeChooz.TechAssessment.Application.Features.Common;
using WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateParticipantsCommand;
using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Queries.GetSessionParticipants;

public class GetSessionParticipantsValidator : AbstractValidator<GetSessionParticipantsFromAdminPageQuery>
{
    public GetSessionParticipantsValidator(ISessionRepositoryAsync sessionRepository)
    {
        RuleFor(p => p.SessionId)
            .SetAsyncValidator(new IdExistsValidator<GetSessionParticipantsFromAdminPageQuery, Session>(sessionRepository));
    }
}