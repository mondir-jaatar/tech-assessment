using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WeChooz.TechAssessment.Application.Interfaces;

namespace WeChooz.TechAssessment.Persistence;

public class UnitOfWork<TContext>(TContext context) : IUnitOfWork, IDisposable where TContext : DbContext
{
    public IDbContextTransaction Transaction;

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default) => await context.SaveChangesAsync(cancellationToken);

    public IDbContextTransaction GetContextTransaction() => Transaction;

    public async Task DisposeAsync()
    {
        await context.DisposeAsync();
    }

    public async Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (context.Database.CurrentTransaction != null)
            return context.Database.CurrentTransaction;

        Transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        return Transaction;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await Transaction.CommitAsync(cancellationToken);
    }

    public async Task CommitAsync(IDbContextTransaction contextTransaction, CancellationToken cancellationToken = default)
    {
        await Transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (context.Database.CurrentTransaction is null)
        {
            return;
        }

        await Transaction.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        DisposeAsync().Wait();
    }
}