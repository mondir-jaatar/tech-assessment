using FluentValidation;
using WeChooz.TechAssessment.Application.Features.Common;
using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateParticipantsCommand;

public class UpdateParticipantsValidator : AbstractValidator<UpdateParticipantsCommand>
{
    public UpdateParticipantsValidator(ISessionRepositoryAsync sessionRepository, IGenericRepositoryAsync<Participant> participantRepository)
    {
        RuleFor(p => p.SessionId)
            .SetAsyncValidator(new IdExistsValidator<UpdateParticipantsCommand, Session>(sessionRepository));

        RuleForEach(property => property.Participants)
            .SetValidator(new UpdateParticipantDtoValidator(participantRepository));
    }
}

public class UpdateParticipantDtoValidator : AbstractValidator<UpdateParticipantDto>
{
    public UpdateParticipantDtoValidator(IGenericRepositoryAsync<Participant> participantRepository)
    {
        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("Le prénom est requis.")
            .MaximumLength(100).WithMessage("Le prénom ne peut pas dépasser 100 caractères.");

        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("Le nom est requis.")
            .MaximumLength(100).WithMessage("Le nom ne peut pas dépasser 100 caractères.");

        RuleFor(p => p.CompanyName)
            .NotEmpty().WithMessage("Le nom de l'entreprise est requis.")
            .MaximumLength(100).WithMessage("Le nom de l'entreprise ne peut pas dépasser 100 caractères.");

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("L’adresse e-mail est requise.")
            .MaximumLength(100).WithMessage("L’adresse e-mail ne peut pas dépasser 100 caractères.")
            .EmailAddress().WithMessage("L’adresse e-mail n’est pas valide.");
    }
}