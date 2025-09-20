using FluentValidation;
using FluentValidation.Validators;
using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Domain.Common;

namespace WeChooz.TechAssessment.Application.Features.Common;

public class IdExistsValidator<T, TEntity>(IGenericRepositoryAsync<TEntity> repository, string message = "Identifiant est invalide.") : IAsyncPropertyValidator<T, Guid> where TEntity : AuditableBaseEntityWithId
{
    public async Task<bool> IsValidAsync(ValidationContext<T> context, Guid value, CancellationToken cancellation)
    {
        return await repository.AnyAsync(p => p.Id == value, cancellation);
    }

    public string GetDefaultMessageTemplate(string errorCode) => message;

    public string Name { get; } = "IdExistsValidator";
}