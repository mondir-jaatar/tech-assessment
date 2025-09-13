using System.Linq.Expressions;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Domain.Common;

namespace WeChooz.TechAssessment.Persistence.Repositories;

public abstract class GenericRepositoryAsync<TEntity> : IGenericRepositoryAsync<TEntity> where TEntity : AuditableBaseEntityWithId
{
    private readonly CourseDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepositoryAsync(CourseDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }
    
    public async Task<TEntity?> GetByIdAsync(Guid id, bool trackChanges = false)
    {
        if (trackChanges)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) => await _dbSet.AddRangeAsync(entities, cancellationToken);

    public void Update(TEntity entity) => _dbContext.Update(entity);

    public void UpdateRange(IEnumerable<TEntity> entities) =>  _dbContext.UpdateRange(entities);

    public void Delete(TEntity entity) => _dbSet.Remove(entity);

    public void DeleteRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => await _dbSet.AnyAsync(predicate, cancellationToken);

    public async Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => await _dbSet.AllAsync(predicate, cancellationToken);

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate is not null)
        {
            return await _dbSet.CountAsync(predicate, cancellationToken);
        }

        return await _dbSet.CountAsync(cancellationToken);
    }

    public void DeleteRangeAsync(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

    public void DetachEntity(TEntity entity) => _dbSet.Entry(entity).State = EntityState.Detached;

    public async Task<List<TEntity>> GetBySpecification(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) => await _dbSet.WithSpecification(specification).ToListAsync(cancellationToken);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
}