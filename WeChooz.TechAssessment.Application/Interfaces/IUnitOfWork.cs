using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace WeChooz.TechAssessment.Application.Interfaces;

public interface IUnitOfWork
{
    Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    IDbContextTransaction GetContextTransaction();
    Task DisposeAsync();
}