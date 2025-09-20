using FluentValidation;
using WeChooz.TechAssessment.Application.Features.Common;
using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateSessionCommand;

public class UpdateSessionValidator : AbstractValidator<UpdateSessionCommand>
{
    public UpdateSessionValidator(ISessionRepositoryAsync sessionRepository, ICourseRepositoryAsync  courseRepository)
    {
        RuleFor(s => s.Id)
            .SetAsyncValidator(new IdExistsValidator<UpdateSessionCommand, Session>(sessionRepository));
        
        RuleFor(s => s.CourseId)
            .SetAsyncValidator(new IdExistsValidator<UpdateSessionCommand, Course>(courseRepository));

        RuleFor(s => s.Duration)
            .GreaterThan(0).WithMessage("La durée de la session doit être supérieur à 0");
    }
}